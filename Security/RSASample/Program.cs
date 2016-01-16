using System.Linq;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace RSASample
{
    class Program
    {
        private CngKey _aliceKey;
        private byte[] _alicePubKeyBlob;

        static void Main()
        {
            var p = new Program();
            p.Run();

        }

        public void Run()
        {
            byte[] document;

            byte[] hash;
            byte[] signature;
            AliceTasks(out document, out hash, out signature);
            BobTasks(document, hash, signature);
        }

        public void AliceTasks(out byte[] data, out byte[] hash, out byte[] signature)
        {
            InitAliceKeys();

            data = Encoding.UTF8.GetBytes("Best greetings from Alice");
            hash = HashDocument(data);
            signature = AddSignatureToHash(hash, _aliceKey);
        }

        public void BobTasks(byte[] data, byte[] hash, byte[] signature)
        {
            CngKey aliceKey = CngKey.Import(_alicePubKeyBlob, CngKeyBlobFormat.GenericPublicBlob);
            if (!IsSignatureValid(hash, signature, aliceKey))
            {
                WriteLine("signature not valid");
                return;
            }
            if (!IsDocumentUnchanged(hash, data))
            {
                WriteLine("document was changed");
                return;
            }
            WriteLine("signature valid, document unchanged");
            WriteLine($"document from Alice: {Encoding.UTF8.GetString(data)}");
        }

        private bool IsSignatureValid(byte[] hash, byte[] signature, CngKey key)
        {
            using (var signingAlg = new RSACng(key))
            {
                return signingAlg.VerifyHash(hash, signature, HashAlgorithmName.SHA384, RSASignaturePadding.Pss);
            }
        }

        private bool IsDocumentUnchanged(byte[] hash, byte[] data)
        {
            byte[] newHash = HashDocument(data);
            return newHash.SequenceEqual(hash);
        }

        private void InitAliceKeys()
        {
            _aliceKey = CngKey.Create(CngAlgorithm.Rsa);
            _alicePubKeyBlob = _aliceKey.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        private byte[] HashDocument(byte[] data)
        {
            using (var hashAlg = SHA384.Create())
            {
                return hashAlg.ComputeHash(data);
            }
        }

        private byte[] AddSignatureToHash(byte[] hash, CngKey key)
        {
            using (var signingAlg = new RSACng(key))
            {
                byte[] signed = signingAlg.SignHash(hash, HashAlgorithmName.SHA384, RSASignaturePadding.Pss);
                return signed;
            }
        }

    }
}
