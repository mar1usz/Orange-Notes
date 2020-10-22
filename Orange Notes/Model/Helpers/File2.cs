using System.IO;
using System.Threading.Tasks;

namespace Orange_Notes.Model.Helpers
{
    public static class File2
    {
        public static async Task ClearAsync(string path)
        {
            await File.WriteAllTextAsync(path, string.Empty);
        }
    }
}
