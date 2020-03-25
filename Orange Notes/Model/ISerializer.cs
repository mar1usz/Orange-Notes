namespace Orange_Notes.Model
{
    public interface ISerializer<T>
    {
        void Serialize(T objToSerialize, string filePath);
        void Deserialize(T objToDeserialize, string filePath);
    }
}
