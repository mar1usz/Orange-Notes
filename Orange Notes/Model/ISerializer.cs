using System.Threading.Tasks;

namespace Orange_Notes.Model
{
    public interface ISerializer<T>
    {
        void Serialize(T objToSerialize, string filePath);
        T Deserialize(string filePath);
        Task SerializeAsync(T objToSerialize, string filePath);
        Task<T> DeserializeAsync(string filePath);
    }
}