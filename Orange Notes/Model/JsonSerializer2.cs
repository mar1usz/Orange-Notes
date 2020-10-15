using Orange_Notes.Model.Helpers;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Orange_Notes.Model
{
    public class JsonSerializer2<T> : ISerializer<T> where T : new()
    {
        public async Task SerializeAsync(T objToSerialize, string filePath)
        {
            using (StreamWriter writer = File.CreateText(filePath))
            {
                await writer.ClearAsync();
            }

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            using (FileStream stream = File.OpenWrite(filePath))
            {
                await JsonSerializer.SerializeAsync(stream, objToSerialize, jsonOptions);
            }
        }

        public async Task<T> DeserializeAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                using (FileStream stream = File.OpenRead(filePath))
                {
                    return await JsonSerializer.DeserializeAsync<T>(stream);
                }
            }
            else
            {
                return new T();
            }
        }

        public void Serialize(T objToSerialize, string filePath)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string jsonString = JsonSerializer.Serialize(objToSerialize, jsonOptions);
            File.WriteAllText(filePath, jsonString);
        }

        public T Deserialize(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<T>(jsonString);
            }
            else
            {
                return new T();
            }
        }
    }
}
