using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.StaticFiles;
using System.Threading;
using System.IO;
using System;

namespace Orange_Notes.Model
{
    public static class GoogleDriveService
    {
        private static DriveService service = null;
        public static string credentialsFilepath { get; set; } = "credentials.json";

        public static void UploadOrUpdateFile(string filePath)
        {
            if(System.IO.File.Exists(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                string driveFileId = GetDriveFileId(fileName);

                if (driveFileId == null) // check if file doesn't exists already
                    UploadFile(filePath);
                else
                    UpdateFile(filePath, driveFileId);
            }
        }

        public static bool Authorize()
        {
            if (System.IO.File.Exists(credentialsFilepath))
            {
                string[] Scopes = { DriveService.Scope.DriveFile, };
                UserCredential credential;

                using (var stream = new FileStream(credentialsFilepath, FileMode.Open, FileAccess.Read))
                {
                    string tokeFilePath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "Scooby Doo",
                    CancellationToken.None,
                    new FileDataStore(tokeFilePath, true)).Result;
                }

                service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Orange Notes",
                });

                return true;
            }
            else
            {
                throw new Exception("Developer, you need your own 'credentials.json' to use google drive features (see github prerequisites).");
            }

            return false;
        }

        public static string UploadFile(string filePath)
        {
            if(service == null)
            {
                Authorize();
            }
            
            if(System.IO.File.Exists(filePath))
            {
                Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(filePath)
                };
                string fileType = GetMimeType(filePath);
                FilesResource.CreateMediaUpload request;

                using (Stream stream = new FileStream(filePath, FileMode.Open))
                {
                    request = service.Files.Create(fileMetadata, stream, fileType);
                    request.Fields = "id";
                    request.Upload();

                    return request.ResponseBody.Id;
                }
            }

            return null;
        }

        public static string UpdateFile(string filePath, string driveFileId)
        {
            if (service == null)
            {
                Authorize();
            }
            
            if (System.IO.File.Exists(filePath) && driveFileId != null)
            {
                Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(filePath)
                };
                string fileType = GetMimeType(credentialsFilepath);
                FilesResource.UpdateMediaUpload request;

                using (Stream stream = new FileStream(filePath, FileMode.Open))
                {
                    request = service.Files.Update(fileMetadata, driveFileId, stream, fileType);
                    request.Upload();

                    return request.ResponseBody.Id;
                }
            }

            return null;

        }

        public static string GetDriveFileId(string driveFileName)
        {
            if (service == null)
            {
                Authorize();
            }

            FilesResource.ListRequest request = service.Files.List();
            FileList driveFileList = request.Execute();

            foreach (var file in driveFileList.Files)
            {
                if (file.Name == driveFileName)
                    return file.Id;
            }

            return null;
        }

        public static string GetMimeType(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(filePath, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
