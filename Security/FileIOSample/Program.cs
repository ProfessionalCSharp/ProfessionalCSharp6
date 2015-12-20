using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileIOSample
{
    public class Program
    {
        public void Main(string[] args)
        {
            EncryptFile("Sample.xml");
        }

        public void EncryptFile(string fileName)
        {
#if DNXCORE50

#else
            File.Encrypt(fileName);
#endif
        }
    }
}
