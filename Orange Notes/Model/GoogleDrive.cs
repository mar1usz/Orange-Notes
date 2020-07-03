﻿using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System;

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
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;
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
    }
}
