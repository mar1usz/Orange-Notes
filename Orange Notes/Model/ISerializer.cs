using System.Threading.Tasks;

namespace Orange_Notes.Model
{
    public interface ISerializer<T>
    {
        Task SerializeAsync(T objToSerialize, string filePath);
        Task<T> DeserializeAsync(string filePath);
    }
}