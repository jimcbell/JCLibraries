using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO.Library
{
    public static class Writer
    {
        public static void WriteText(string decryptedText, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(decryptedText);
            }
        }
    }
}
