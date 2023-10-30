using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsuranceERP.Core.Utilities
{
    public static class CryptographyExtensions
    {

        static string aesKey = "vBFHy7xkpfKMPTUS";
        static string ivKey = "yg4PHjOfzm+OK/01";
        public static string Encrypt(this string plainText)
        {
            string cipherText = null;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(aesKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(ivKey);

                // Check arguments.
                if (string.IsNullOrWhiteSpace(plainText) || plainText.Length <= 0)
                    throw new ArgumentNullException("plainText");
                if (aesAlg.Key == null || aesAlg.Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (aesAlg.IV == null || aesAlg.IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                // Create an encryptor to perform the stream transform.
                using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Create the streams used for encryption.
                    using (var msEncrypt = new MemoryStream())
                    {
                        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }

                        var encrypted = msEncrypt.ToArray();

                        // Covert ByteArray To String
                        var hex = new StringBuilder(encrypted.Length * 2);
                        foreach (byte b in encrypted)
                        {
                            hex.AppendFormat("{0:x2}", b);
                        }
                        cipherText = hex.ToString();
                    }
                }
            }

            return cipherText;
        }

        public static string Decrypt(string cipherText)
        {
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(aesKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(ivKey);

                // Check arguments. 
                if (string.IsNullOrWhiteSpace(cipherText) || cipherText.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (aesAlg.Key == null || aesAlg.Key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (aesAlg.IV == null || aesAlg.IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                // Covert Hexadecimal String To ByteArray;
                var outputLength = cipherText.Length / 2;
                var output = new byte[outputLength];
                using (var sr = new StringReader(cipherText))
                {
                    for (var i = 0; i < outputLength; i++)
                        output[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
                }

                byte[] cipherBytes = output;

                using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                {
                    // Create the streams used for decryption
                    using (var msDecrypt = new MemoryStream(cipherBytes))
                    {
                        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            //Read the decrypted bytes from the decrypting stream and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }

}