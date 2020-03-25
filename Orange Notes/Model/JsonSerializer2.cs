using System.IO;
using System.Text.Json;

namespace Orange_Notes.Model
{
    public class JsonSerializer2<T> : ISerializer<T>
    {
        public void Serialize(T objToSerialize, string filePath)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.WriteIndented = true;

            string jsonString = JsonSerializer.Serialize(objToSerialize, jsonOptions);
            File.WriteAllText(filePath, jsonString);
        }

        public void Deserialize(T objToDeserialize, string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                objToDeserialize = JsonSerializer.Deserialize<T>(jsonString);
            }
        }
    }
}
