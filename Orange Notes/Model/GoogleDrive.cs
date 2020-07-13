using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System;
using System.Threading.Tasks;
using Google.Apis.Drive.v3.Data;

namespace Orange_Notes.Model
{
    public static class GoogleDrive<T> where T : new()
    {
        private static DriveService service = null;

        public static void Authorize(string credentialsFilePath)
        {
            string[] Scopes = { DriveService.Scope.DriveFile };
            string ApplicationName = "Orange Notes";
            UserCredential credential;

            using (var stream = new FileStream(credentialsFilePath, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "Scooby Doo",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            service.HttpClient.Timeout = TimeSpan.FromSeconds(120);
        }

        public static void UploadFile(T objToUpload, string fileName)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(objToUpload, jsonOptions);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName
            };
            string driveFileId = GetDriveFileId(fileName);

            if (driveFileId == null)
            {
                FilesResource.CreateMediaUpload request;
                using (Stream stream = new MemoryStream(jsonBytes))
                {
                    request = service.Files.Create(fileMetadata, stream, "application/json");
                    request.Fields = "id";
                    request.Upload();
                }
            }
            else
            {
                FilesResource.UpdateMediaUpload request;
                using (Stream stream = new MemoryStream(jsonBytes))
                {
                    request = service.Files.Update(fileMetadata, driveFileId, stream, "application/json");
                    request.Fields = "id";
                    request.Upload();
                }
            }
        }

        public static T DownloadFile(string fileName)
        {
            string driveFileId = GetDriveFileId(fileName);
            if (driveFileId == null)
            {
                return new T();
            }
            else
            {
                FilesResource.GetRequest request;
                using (Stream stream = new MemoryStream())
                {
                    request = service.Files.Get(driveFileId);
                    request.Fields = "id";
                    request.DownloadWithStatus(stream);

                    stream.Position = 0;
                    StreamReader reader = new StreamReader(stream);
                    return JsonSerializer.Deserialize<T>(reader.ReadToEnd());
                }
            }
        }

        public static string GetDriveFileId(string fileName)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
            foreach (var f in files)
            {
                if (f.Name == fileName)
                    return f.Id;
            }
            return null;
        }

        public static async Task AuthorizeAsync(string credentialsFilePath)
        {
            if (service != null)
                return;

            string[] Scopes = { DriveService.Scope.DriveFile };
            string ApplicationName = "Orange Notes";
            UserCredential credential;

            using (var stream = new FileStream(credentialsFilePath, FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                // ?? AuthorizeAsync doesn't run asynchronously
                credential = await Task.Run(() => GoogleWebAuthorizationBroker.AuthorizeAsync(
                    stream,
                    Scopes,
                    "Scooby Doo",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)));
            }

            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }

        public static async Task UploadFileAsync(T objToUpload, string fileName)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            byte[] jsonBytes = JsonSerializer.SerializeToUtf8Bytes(objToUpload, jsonOptions);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName
            };

            string driveFileId = await GetDriveFileIdAsync(fileName);
            if (driveFileId == null)
            {
                FilesResource.CreateMediaUpload request;
                using (Stream stream = new MemoryStream(jsonBytes))
                {
                    request = service.Files.Create(fileMetadata, stream, "application/json");
                    request.Fields = "id";
                    await request.UploadAsync();
                }
            }
            else
            {
                FilesResource.UpdateMediaUpload request;
                using (Stream stream = new MemoryStream(jsonBytes))
                {
                    request = service.Files.Update(fileMetadata, driveFileId, stream, "application/json");
                    request.Fields = "id";
                    await request.UploadAsync();
                }
            }
        }

        public static async Task<T> DownloadFileAsync(string fileName)
        {
            string driveFileId = await GetDriveFileIdAsync(fileName);
            if (driveFileId == null)
            {
                return new T();
            }
            else
            {
                FilesResource.GetRequest request;
                using (Stream stream = new MemoryStream())
                {
                    request = service.Files.Get(driveFileId);
                    request.Fields = "id";
                    await request.DownloadAsync(stream);
                    stream.Position = 0;
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
        }

        public static async Task<string> GetDriveFileIdAsync(string fileName)
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            FileList listRequestResult = await listRequest.ExecuteAsync();
            IList<Google.Apis.Drive.v3.Data.File> files = listRequestResult.Files;
            foreach (var f in files)
            {
                if (f.Name == fileName)
                    return f.Id;
            }
            return null;
        }
    }
}
