using System.IO;
using System.Threading.Tasks;

namespace Orange_Notes.Model.Helpers
{
    public static class Extensions
    {
        public static Task ClearAsync(this StreamWriter writer)
        {
            return writer.WriteAsync(string.Empty);
        }
    }
}
