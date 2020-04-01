using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Orange_Notes.Model
{
    public static class GoogleDrive<T> where T : new()
    {
        private static DriveService service = null;

        public static void Authorize(string credentialsFilePath)
        {
            if (!System.IO.File.Exists(credentialsFilePath))
                throw new FileNotFoundException("You need 'credentials.json' to run this (see github prerequistses)");

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
        }

        public static void UploadFile(T objToUpload, string fileName)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;
            string jsonString = JsonSerializer.Serialize(objToUpload, jsonOptions);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = fileName
            };
            string driveFileId = GetDriveFileId(fileName);

            if (driveFileId == null)
            {
                FilesResource.CreateMediaUpload request;
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    request = service.Files.Create(fileMetadata, stream, "application/json");
                    request.Fields = "id";
                    request.Upload();
                }
            }
            else
            {
                FilesResource.UpdateMediaUpload request;
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
                {
                    request = service.Files.Update(fileMetadata, driveFileId, stream, "application/json");
                    request.Fields = "id";
                    request.Upload();
                }
            }
        }

        public static void DownloadFile(ref T objToDownload, string fileName)
        {
            string driveFileId = GetDriveFileId(fileName);

            if (driveFileId == null)
            {
                objToDownload = new T();
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
                    StreamReader streamR = new StreamReader(stream);
                    objToDownload = JsonSerializer.Deserialize<T>(streamR.ReadToEnd());
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
    }
}
