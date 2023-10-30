using HealthInsuranceERP.Core.Interfaces.Services;
using HealthInsuranceERP.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HealthInsuranceERP.Infrastructure.Services
{
    public class UtilityService : IUtilityService
    {
        UtilityConfig utilityConfig1 = new UtilityConfig();
        private byte[] IV = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
        private int BlockSize = 128;
        private readonly UtilityConfig _utilityConfig;
        private readonly ILoggerService<UtilityService> _loggerService;
        public UtilityService( ILoggerService<UtilityService> loggerService
            )
        {

            _loggerService = loggerService;
            _utilityConfig = utilityConfig1;
        }

        public string ToSha256(string value)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] byteString = Encoding.UTF8.GetBytes(value);
                byte[] shaBytes = sha256.ComputeHash(byteString);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < shaBytes.Length; i++)
                {
                    builder.Append(shaBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string EncryptAes(string plainText)
        {
            try
            {
                if (string.IsNullOrEmpty(plainText))
                    return null;
                byte[] bytes = Encoding.UTF8.GetBytes(plainText.Trim());

                using (SymmetricAlgorithm crypt = Aes.Create())
                {
                    HashAlgorithm hash = MD5.Create();
                    crypt.BlockSize = BlockSize;
                    crypt.Key = hash.ComputeHash(Encoding.UTF8.GetBytes(_utilityConfig.AesKey));
                    crypt.IV = IV;
                    string cipherText = null;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cryptoStream.Write(bytes, 0, bytes.Length);
                        }

                        cipherText = Convert.ToBase64String(memoryStream.ToArray());
                    }
                    return cipherText;
                }
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return null;
            }
        }

        public string DecryptAes(string cipherText)
        {
            try
            {
                string decryptedText = "";
                if (string.IsNullOrEmpty(cipherText)) return null;
                byte[] bytes = Convert.FromBase64String(cipherText);

                using (SymmetricAlgorithm crypt = Aes.Create())
                {
                    HashAlgorithm hash = MD5.Create();
                    crypt.Key = hash.ComputeHash(Encoding.UTF8.GetBytes(_utilityConfig.AesKey));
                    crypt.IV = IV;

                    using (MemoryStream memoryStream = new MemoryStream(bytes))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, crypt.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            byte[] decryptedBytes = new byte[bytes.Length];
                            cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                            decryptedText = Encoding.UTF8.GetString(decryptedBytes);
                        }
                    }
                }

                return decryptedText.Replace("\0", String.Empty).Trim(); ;
            }
            catch (Exception ex)
            {
                _loggerService.LogError(ex);
                return null;
            }
        }

        //public EnvMode AppEnv()
        //{
        //    if (authConfig.IsLive)
        //        return EnvMode.Live;

        //    return EnvMode.Test;
        //}

    }
}
