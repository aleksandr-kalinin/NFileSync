using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileSync
{
    class Program
    {
        private static void MarkFilesToSync(string path, string primExt, string secExt, string syncExt )
        {
            string[] fileNamesPrim = Directory.GetFiles(path, "*"+primExt);

            foreach (var fileName in fileNamesPrim)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                var name = fileInfo.Name;
                var prevIndex = 0;
                var index = 0;
                do
                {
                    index = name.IndexOf(primExt, prevIndex + 1, StringComparison.OrdinalIgnoreCase);
                    if (index > 0)
                    {
                        prevIndex = index;
                    }
                } while (index > 0);

                name = name.Remove(prevIndex);
                var fileSec = Path.Combine(path, name + secExt);
                var filePrim = Path.Combine(path, name + primExt);


                if (!File.Exists(fileSec))
                {
                    File.Move(filePrim, filePrim + syncExt);
                }
            }

        }

        static void Main(string[] args)
        {
            if (args.Length==0)
            {
                Console.WriteLine("Usage: FileSync PathWithFiles.");
                return;
            }

            string path = args[0];
            MarkFilesToSync(path, ".jpg", ".cr2", ".sync");
            MarkFilesToSync(path, ".cr2", ".jpg", ".sync");
        }
    }
}
