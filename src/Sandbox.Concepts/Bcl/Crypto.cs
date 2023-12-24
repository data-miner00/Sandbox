namespace Sandbox.Concepts.Bcl
{
    using System.Security.Cryptography;

    public static class Crypto
    {
        // Symmetric encryption - encrypt large amount of data
        // Asymmetric encryption - encrypt small amount of data
        public static void SymmetricEncryptData(string filename)
        {
            using var fileStream = new FileStream(filename, FileMode.OpenOrCreate);
            using var aes = Aes.Create();

            byte[] key = [
                0x01,
                0x02,
                0x03,
                0x04,
                0x05,
                0x06,
                0x07,
                0x08,
                0x09,
                0x10,
                0x11,
                0x12,
                0x13,
                0x14,
                0x15,
                0x16,
            ];

            var iv = aes.IV;
            fileStream.Write(iv, 0, iv.Length);

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
            using var encryptWriter = new StreamWriter(cryptoStream);

            encryptWriter.WriteLine("Hello WOrld");
        }

        public static void AsymmetricEncryptData(string filename)
        {
            // Initialize the byte arrays to the public key information
            byte[] modulus = [214, 46, 220, 83, 160, 73, 40, 39];
            byte[] exponent = { 1, 0, 1 };

            // Create values to store encrypted symmetric keys
            byte[] encryptedSymmetricKey;
            byte[] encryptedSymmetricIV;

            // Create a new instance of the RSA class
            RSA rsa = RSA.Create();

            // Create a new instance of the RSAParameters
            var rsaKeyInfo = new RSAParameters();

            // Set rsaKeyInfo to the public key values
            rsaKeyInfo.Modulus = modulus;
            rsaKeyInfo.Exponent = exponent;

            // import key parameters into rsa
            var aes = Aes.Create();

            // Encrypt the symmetric key and IV
            encryptedSymmetricIV = rsa.Encrypt(aes.Key, RSAEncryptionPadding.Pkcs1);
            encryptedSymmetricIV = rsa.Encrypt(aes.IV, RSAEncryptionPadding.Pkcs1);
        }

        public static void SymmetricDecryptData(string filename)
        {
            using var fileStream = new FileStream(filename, FileMode.Open);
            using var aes = Aes.Create();

            byte[] iv = new byte[aes.IV.Length];
            int numBytesToRead = aes.IV.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                if (n == 0)
                {
                    break;
                }

                numBytesRead += n;
                numBytesToRead -= n;
            }

            byte[] key = [
                0x01,
                0x02,
                0x03,
                0x04,
                0x05,
                0x06,
                0x07,
                0x08,
                0x09,
                0x10,
                0x11,
                0x12,
                0x13,
                0x14,
                0x15,
                0x16,
            ];

            using var cryptoStream = new CryptoStream(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            using var decryptReader = new StreamReader(cryptoStream);

            var decryptedMessage = decryptReader.ReadToEnd();
            Console.WriteLine(decryptedMessage);
        }
    }
}
