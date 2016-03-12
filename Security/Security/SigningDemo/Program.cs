using System;
using System.Security.Cryptography;
using System.Text;
using static System.Console;

namespace SigningDemo
{
    class Program
    {

        CngKey _aliceKeySignature;
        private byte[] _alicePubKeyBlob;

        static void Main()
        {
            var p = new Program();
            p.Run();
        }

        public void Run()
        {
            InitAliceKeys();

            byte[] aliceData = Encoding.UTF8.GetBytes("Alice");
            byte[] aliceSignature = CreateSignature(aliceData, _aliceKeySignature);
            WriteLine($"Alice created signature: {Convert.ToBase64String(aliceSignature)}");

            if (VerifySignature(aliceData, aliceSignature, _alicePubKeyBlob))
            {
                WriteLine("Alice signature verified successfully");
            }
        }

        public void InitAliceKeys()
        {
            _aliceKeySignature = CngKey.Create(CngAlgorithm.ECDsaP521);
            _alicePubKeyBlob = _aliceKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
        }

        private byte[] CreateSignature(byte[] data, CngKey key)
        {

            byte[] signature;
            using (var signingAlg = new ECDsaCng(key))
            {
#if NET46
                signature = signingAlg.SignData(data);
                signingAlg.Clear();
#else
                signature = signingAlg.SignData(data, HashAlgorithmName.SHA512);

#endif
            }
            return signature;
        }

        private bool VerifySignature(byte[] data, byte[] signature, byte[] pubKey)
        {
            bool retValue = false;
            using (CngKey key = CngKey.Import(pubKey, CngKeyBlobFormat.GenericPublicBlob))
            using (var signingAlg = new ECDsaCng(key))
            {
#if NET46
                retValue = signingAlg.VerifyData(data, signature);
                signingAlg.Clear();
#else
                retValue = signingAlg.VerifyData(data, signature, HashAlgorithmName.SHA512);
#endif
            }
            return retValue;
        }
    }
}
