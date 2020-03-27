using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;

namespace Orange_Notes.Model
{
    public class DriveSerializer<T> : ISerializer<T>
    {
        DriveService service;

        public DriveSerializer()
        {
            // TODO
            //service = ..
        }

        public void Serialize(T objToSerialize, string filePath)
        {
            Google.Apis.Drive.v3.Data.File fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath)
            };
            string fileType = GetMimeType(filePath);
            FilesResource.CreateMediaUpload request;
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                request = service.Files.Create(fileMetadata, stream, fileType);
                request.Fields = "id";
                request.Upload();
            }
            // TODO
        }

        public void Deserialize(T objToDeserialize, string filePath)
        {
            // TODO
        }

        public static string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
