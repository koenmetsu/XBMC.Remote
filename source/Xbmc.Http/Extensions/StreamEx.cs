using System.IO;
using System.Threading.Tasks;

namespace Xbmc.Http.Extensions
{
    static class StreamEx
    {
         public static byte[] ReadAsBytes(this Stream stream)
         {
             byte[] buffer = new byte[16 * 1024];
             using (MemoryStream ms = new MemoryStream())
             {
                 int read;
                 while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                 {
                     ms.Write(buffer, 0, read);
                 }
                 return ms.ToArray();
             }
         }
    }
}