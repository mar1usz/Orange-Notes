using System.IO;
using System.Threading.Tasks;

namespace Orange_Notes.Model.Helpers
{
    public static class File2
    {
        public static Task ClearAsync(string path)
        {
            return File.WriteAllTextAsync(path, string.Empty);
        }
    }
}
