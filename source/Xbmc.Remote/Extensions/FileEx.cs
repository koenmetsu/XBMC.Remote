using System.IO;
using System.IO.IsolatedStorage;

namespace Xbmc.Remote.Extensions
{
    public class FileEx
    {
        public static string ReadAllText(IsolatedStorageFile storage, string path)
        {
        	using (IsolatedStorageFileStream fileStream = storage.OpenFile(path, FileMode.Open))
            {
                var reader = new StreamReader(fileStream);
                return reader.ReadToEnd();
            }
        }
    }
}
