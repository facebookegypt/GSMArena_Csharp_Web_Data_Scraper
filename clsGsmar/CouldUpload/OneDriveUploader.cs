using Azure.Identity;
using clsGsmar.Credentials;
using Microsoft.Graph;
using Microsoft.Graph.Drives;
using Microsoft.Graph.Drives.Item;
using Microsoft.Graph.Drives.Item.Items.Item.CreateUploadSession;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace clsGsmar.CloudUpload
{
    public class OneDriveUploader
    {
        private DriveItem? targetFolder;
        private static GraphServiceClient GetGraphClient()
        {
            var options = new InteractiveBrowserCredentialOptions
            {
                ClientId = GraphConfig.ClientId,
                TenantId = GraphConfig.TenantId,
                RedirectUri = new Uri("http://localhost/")
            };

            var credential = new InteractiveBrowserCredential(options);
            return new GraphServiceClient(credential, GraphConfig.Scopes);
        }
        public async Task<string> GetDriveIdAsync(GraphServiceClient graphClient)
        {
            var drive = await graphClient.Me.Drive.GetAsync();
            return drive?.Id ?? throw new Exception("Unable to retrieve drive ID.");
        }
        public async Task UploadFileAsync(string filePath)
        {
            var graphClient = GetGraphClient(); // Your own method to initialize GraphServiceClient
            var fileName = Path.GetFileName(filePath);

            // 1. Get the OneDrive Drive ID
            var driveId = await GetDriveIdAsync(graphClient);

            // 2. Define app folder path inside OneDrive's special Apps folder
            var appFolderPath = Properties.evry1falls.Default.AppFolderName;

            try
            {
                // 3. Try to get the subfolder if it exists
                targetFolder = await graphClient
                    .Drives[driveId]
                    .Root
                    .ItemWithPath(appFolderPath)
                    .GetAsync();

                Debug.WriteLine($"Found folder '{appFolderPath}' in OneDrive.");
            }
            catch (ODataError ex) when (ex.ResponseStatusCode == 404)
            {
                // 4. Create the subfolder if it doesn't exist
                var folderParts = appFolderPath.Split('/');
                string currentPath = "";
                DriveItem? parentFolder = await graphClient.Drives[driveId].Root.GetAsync();

                foreach (var folderName in folderParts)
                {
                    currentPath += "/" + folderName;
                    try
                    {
                        targetFolder = await graphClient
                            .Drives[driveId]
                            .Root
                            .ItemWithPath(currentPath)
                            .GetAsync();
                    }
                    catch (ODataError ex2) when (ex2.ResponseStatusCode == 404)
                    {
                        var newFolder = new DriveItem
                        {
                            Name = folderName,
                            Folder = new Folder(),
                            AdditionalData = new Dictionary<string, object>
                    {
                        { "@microsoft.graph.conflictBehavior", "rename" }
                    }
                        };

                        targetFolder = await graphClient
                            .Drives[driveId]
                            .Root
                            .ItemWithPath(currentPath.Substring(0, currentPath.LastIndexOf("/")))
                            .Children
                            .PostAsync(newFolder);

                        Debug.WriteLine($"📁 Created folder '{folderName}' at path '{currentPath}'");
                    }
                }
            }

            if (targetFolder == null || string.IsNullOrEmpty(targetFolder.Id))
                throw new Exception("❌ Failed to get or create target subfolder.");

            // 5. Setup upload session
            var requestBody = new CreateUploadSessionPostRequestBody
            {
                Item = new DriveItemUploadableProperties
                {
                    AdditionalData = new Dictionary<string, object>
            {
                { "@microsoft.graph.conflictBehavior", "rename" }
            }
                }
            };

            var uploadSession = await graphClient
                .Drives[driveId]
                .Items[targetFolder.Id]
                .ItemWithPath(fileName)
                .CreateUploadSession
                .PostAsync(requestBody);

            if (uploadSession == null || string.IsNullOrEmpty(uploadSession.UploadUrl))
                throw new Exception("❌ Failed to create upload session.");

            // 6. Upload in chunks
            const int maxSliceSize = 320 * 1024; // 320 KB
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            var uploadTask = new LargeFileUploadTask<DriveItem>(uploadSession, fileStream, maxSliceSize);

            IProgress<long> progress = new Progress<long>(prog =>
            {
                Console.WriteLine($"Uploading {prog} / {fileStream.Length} bytes...");
            });

            try
            {
                var result = await uploadTask.UploadAsync(progress);

                if (result.UploadSucceeded)
                {
                    Console.WriteLine("✅ Upload completed!");
                    Debug.WriteLine("File URL: " + result.ItemResponse?.WebUrl);
                }
                else
                {
                    Console.WriteLine("❌ Upload failed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Upload error: {ex.Message}");
            }
        }

    }
}