using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Upload;

namespace Orange_Notes.Model
{
    public class GoogleDriveSerializer<T> : ISerializer<T> where T : new()
    {
        private DriveService service = null;
        private readonly string credentials_filePath = "credentials.json";
        private readonly string token_filePath = "token.json";

        private async Task AuthorizeAsync()
        {
            if (service != null)
                return;

            string[] scopes = { DriveService.Scope.DriveFile };
            string applicationName = "Orange Notes";
            UserCredential credential;

            using (var stream = new FileStream(credentials_filePath, FileMode.Open, FileAccess.Read))
            {
                // ?? AuthorizeAsync doesn't run asynchronously
                credential = await Task.Run(() => GoogleWebAuthorizationBroker.AuthorizeAsync(
                    stream,
                    scopes,
                    "Scooby Doo",
                    CancellationToken.None,
                    new FileDataStore(token_filePath, true)));
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName,
            });
            service.HttpClient.Timeout = TimeSpan.FromSeconds(120);
        }

        public async Task SerializeAsync(T objToSerialize, string fileName)
        {
            await AuthorizeAsync();

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName
            };

            string driveFileId = await GetDriveFileIdAsync(fileName);
            using (Stream stream = new MemoryStream())
            {
                JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                await JsonSerializer.SerializeAsync(stream, objToSerialize, jsonOptions);
                stream.Position = 0;
                ResumableUpload request;
                {
                    if (driveFileId == null)
                    {
                        request = new FilesResource.CreateMediaUpload(service, fileMetadata, stream, "application/json");
                    }
                    else
                    {
                        request = new FilesResource.UpdateMediaUpload(service, fileMetadata, driveFileId, stream, "application/json");
                    }
                    await request.UploadAsync();
                }
            }
        }

        public async Task<T> DeserializeAsync(string fileName)
        {
            await AuthorizeAsync();

            string driveFileId = await GetDriveFileIdAsync(fileName);
            if (driveFileId == null)
            {
                return new T();
            }
            else
            {
                FilesResource.GetRequest request = new FilesResource.GetRequest(service, driveFileId);

                using (Stream stream = new MemoryStream())
                {
                    await request.DownloadAsync(stream);
                    stream.Position = 0;
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
        }

        private async Task<string> GetDriveFileIdAsync(string fileName)
        {
            await AuthorizeAsync();

            FilesResource.ListRequest listRequest = service.Files.List();
            FileList listRequestResult = await listRequest.ExecuteAsync();

            foreach (var f in listRequestResult.Files)
            {
                if (f.Name == fileName)
                    return f.Id;
            }
            return null;
        }
    }
}
