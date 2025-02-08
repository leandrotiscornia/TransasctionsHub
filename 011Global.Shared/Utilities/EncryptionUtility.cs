using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.SecretServiceConnection;
using System.Security.Cryptography;
using _011Global.Shared.JobsServiceDBContext.Entities;

namespace _011Global.Shared.Utilities
{
    public static class EncryptionUtility
    {

        public static async Task<string> AES256GenerateInicializationVector(int id)
        {
            string iv;
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateIV();
                iv = Convert.ToBase64String(aes.IV);
            }
            await SecretServiceAccess.SaveAESCreditCardIV(iv, id);
            return iv;
        }

        public static string AES256Encrypt(string toEncrypt, string key, string iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] encryptedBytes;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(toEncrypt);
                        cs.Write(bytes, 0, bytes.Length);
                    }
                    encryptedBytes = ms.ToArray();

                }
                return Convert.ToBase64String(encryptedBytes);
            }
        }
        public static string AES256Decrypt(string toDecrypt, string key, string iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(key);
                aes.IV = Convert.FromBase64String(iv);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] bytesToDecrypt = Convert.FromBase64String(toDecrypt);
                byte[] decryptedBytes;
                using (var ms = new MemoryStream(bytesToDecrypt))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var msText = new MemoryStream())
                        {
                            cs.CopyTo(msText);
                            decryptedBytes = msText.ToArray();
                        }
                    }

                }
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}