using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSATest
{
    public class Program
    {
        public void Main(string[] args)
        {
            CngKey k = CngKey.Create(CngAlgorithm.Rsa);
            RSACng r = new RSACng(k);
            r.VerifyData()
            byte[] data = Encoding.UTF8.GetBytes("Hello, RSA!");
            RSAEncryptionPadding padding = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);
            byte[] encryptedData = r.Encrypt(data, padding);
            byte[] encryptedRead = r.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
            string read = Encoding.UTF8.GetString(encryptedRead);
            Console.WriteLine(read);
        }
    }
}
