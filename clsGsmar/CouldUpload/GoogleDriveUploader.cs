using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;
using File = System.IO.File;

namespace clsGsmar.CouldUpload
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Auth.OAuth2.Flows;
    using Google.Apis.Auth.OAuth2.Responses;
    using Google.Apis.Drive.v3;
    using Google.Apis.Services;
    using Google.Apis.Upload;
    using Google.Apis.Util;
    using Google.Apis.Util.Store;
    using System.Diagnostics;
    using System.Net;
    using System.Text.Json;

    namespace clsGsmar.CloudUpload
    {
        public class GoogleDriveUploader
        {
            // Paths (ensure G_Credentials.json is copied to output in UI project)
            private static readonly string CredentialsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "G_Credentials.json");
            private static readonly string TokenPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "G_Token.json");

            // Scope: allow the app to create files the user can access (drive file)
            private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile };

            /// <summary>
            /// Get or create a UserCredential. Uses saved token if present; otherwise performs browser OAuth.
            /// Tokens are persisted to G_Token.json (access + refresh).
            /// </summary>
            [Obsolete]
            private async Task<UserCredential> GetUserCredentialAsync()
            {
                if (!File.Exists(CredentialsPath))
                    throw new FileNotFoundException("Google credentials file not found: " + CredentialsPath);

                // Load client secrets from credentials json
                using var credStream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read);
                var googleSecrets = GoogleClientSecrets.FromStream(credStream);

                // Build an auth flow
                var initializer = new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = googleSecrets.Secrets,
                    Scopes = Scopes
                };
                var flow = new GoogleAuthorizationCodeFlow(initializer);

                // If token file exists, try to use it
                TokenResponse? token = null;
                if (File.Exists(TokenPath))
                {
                    var tokenJson = await File.ReadAllTextAsync(TokenPath);
                    try
                    {
                        token = JsonSerializer.Deserialize<TokenResponse>(tokenJson);
                    }
                    catch
                    {
                        token = null;
                    }
                }

                // If we have a token, create and (if needed) refresh it
                if (token != null)
                {
                    var cred = new UserCredential(flow, "user", token);

                    // Attempt refresh if it's expired and refresh token is available
                    if (cred.Token.IsExpired(SystemClock.Default) && !string.IsNullOrWhiteSpace(cred.Token.RefreshToken))
                    {
                        bool ok = await cred.RefreshTokenAsync(CancellationToken.None);
                        if (ok)
                        {
                            // Persist refreshed token
                            await File.WriteAllTextAsync(TokenPath, JsonSerializer.Serialize(cred.Token));
                        }
                    }
                    return cred;
                }

                // No token -> perform interactive auth
                // Use the redirect URI from credentials (must match what you registered)
                var redirectUri = "http://localhost:5000/"; // googleSecrets.Secrets.RedirectUris?.FirstOrDefault()
                                  //?? "http://localhost:5000/";

                // Build the authorization URL
                var authUrl = flow.CreateAuthorizationCodeRequest(redirectUri).Build();

                // Start local HTTP listener on that exact redirect URI (ensure trailing slash)
                using var listener = new HttpListener();
                listener.Prefixes.Add(redirectUri); // e.g. "http://localhost:5000/"
                listener.Start();

                // Open browser for user interaction
                Process.Start(new ProcessStartInfo
                {
                    FileName = authUrl.ToString(),
                    UseShellExecute = true
                });

                // Wait for the callback and retrieve the "code" query param
                var context = await listener.GetContextAsync();
                var code = context.Request.QueryString["code"];

                // Give a simple response page to the user's browser
                var responseHtml = "<html><body><h2>Authentication complete. You can close this window.</h2></body></html>";
                var buffer = System.Text.Encoding.UTF8.GetBytes(responseHtml);
                context.Response.ContentLength64 = buffer.Length;
                await context.Response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
                listener.Stop();

                if (string.IsNullOrWhiteSpace(code))
                    throw new Exception("Authorization code not received from Google.");

                // Exchange code for tokens
                var tokenResponse = await flow.ExchangeCodeForTokenAsync("user", code!, redirectUri, CancellationToken.None);

                // Save token to disk for future runs (access + refresh)
                await File.WriteAllTextAsync(TokenPath, JsonSerializer.Serialize(tokenResponse));

                // Construct credential that DriveService will use
                var credential = new UserCredential(flow, "user", tokenResponse);
                return credential;
            }

            /// <summary>
            /// Upload a file to Google Drive (DriveFile scope: file created is owned by the user and visible in their Drive).
            /// </summary>
            /// <param name="filePath">Full path to local file</param>
            /// <param name="progress">Optional progress reporter (bytes uploaded)</param>
            public async Task<string> UploadFileAsync(string filePath, IProgress<string>? progress = null)
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("File to upload not found.", filePath);

                var credential = await GetUserCredentialAsync();

                using var driveService = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "GSMArenaUploader"
                });

                var fileName = Path.GetFileName(filePath);
                var fileMetadata = new Google.Apis.Drive.v3.Data.File { Name = fileName };

                using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var request = driveService.Files.Create(fileMetadata, stream, "application/octet-stream");
                request.Fields = "id,webViewLink";

                // Upload and wait
                var uploadProgress = await request.UploadAsync();

                if (uploadProgress.Status == UploadStatus.Completed && request.ResponseBody != null)
                {
                    var webLink = request.ResponseBody.WebViewLink;
                    progress?.Report($"Uploaded: {webLink}");
                    return webLink ?? string.Empty;
                }

                throw new Exception(uploadProgress.Exception?.Message ?? "Upload failed.");
            }
        }
    }
}
