using Dropbox.Api;
using Dropbox.Api.Common;
using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using Dropbox.Api.Team;
using Dropbox.Api.Users;
using System.Configuration;
using System.Diagnostics;
using System.Net;

namespace clsGsmar.CloudUpload
{
    public class DropBoxUploader
    {
        //I am using a seprate json file in the UI Project to cover my credentials, you can
        //add yours here while testing.
        //private const string AppKey = "xxxxx";       // From Dropbox App Console
        //private const string AppSecret = "xxxxx"; // From Dropbox App Console
        //private const string RedirectUri =  "http://localhost:XXXX/authorize"; // Must match your app settings

        public string AccessToken { get; private set; } = string.Empty;
        private DropboxClient dropboxClient;
        public async Task<bool> AuthenticateAsync()
        {
            var creds = DropboxCredentials.Load();
            string AppKey = creds.AppKey;
            string AppSecret = creds.AppSecret;
            string RedirectUri = creds.RedirectUri;

            var state = Guid.NewGuid().ToString("N");
            var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(
                OAuthResponseType.Code,
                AppKey,
                new Uri(RedirectUri),
                state: state);

            using var listener = new HttpListener();
            listener.Prefixes.Add(RedirectUri + "/");
            listener.Start();

            Process.Start(new ProcessStartInfo
            {
                FileName = authorizeUri.ToString(),
                UseShellExecute = true
            });

            var context = await listener.GetContextAsync();
            var response = context.Response;

            var query = context.Request.QueryString;
            var code = query["code"];
            var receivedState = query["state"];

            string responseHtml = "<html><body><h2>You can close this window.</h2></body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseHtml);
            response.ContentLength64 = buffer.Length;
            await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
            response.Close();
            listener.Stop();

            if (string.IsNullOrEmpty(code) || receivedState != state)
                return false;

            var tokenResult = await DropboxOAuth2Helper.ProcessCodeFlowAsync(
                code,
                AppKey,
                AppSecret,RedirectUri,client: null,codeVerifier: null);

            AccessToken = tokenResult.AccessToken;
            dropboxClient = new DropboxClient(AccessToken);

            return !string.IsNullOrEmpty(AccessToken);
        }
        public async Task<bool> UploadFileAsync(string localPath, string dropboxFileName = null)
        {
            if (dropboxClient == null)
                throw new InvalidOperationException("Client not authenticated. Call AuthenticateAsync first.");

            dropboxFileName ??= Path.GetFileName(localPath);

            using (var fileStream = File.OpenRead(localPath))
            {
                var metadata = await dropboxClient.Files.UploadAsync(
                    "/" + dropboxFileName,
                    WriteMode.Overwrite.Instance,
                    body: fileStream
                );
                return metadata != null;
            }
        }
    }

}

