using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace TestApp
{
    public static class AesHelper
    {
        public static string Encrypt(string aesKey, string plainText)
        {
            Console.WriteLine("加密开始====>");
            string decryptText = "";
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(aesKey));
                var rd = sha1.ComputeHash(hash);
                byte[] keyArray = rd.Take(16).ToArray();

                byte[] plainByte = Encoding.UTF8.GetBytes(plainText);
                Base64Encoder encoder = new Base64Encoder();
                RijndaelManaged AesCipher = new RijndaelManaged();
                AesCipher.KeySize = 128; // 192, 256
                AesCipher.BlockSize = 128;
                AesCipher.Mode = CipherMode.ECB;
                AesCipher.Padding = PaddingMode.PKCS7;
                //AesCipher.IV = keyArray;
                AesCipher.Key = keyArray;
                ICryptoTransform crypto = AesCipher.CreateEncryptor();
                byte[] cipherText = crypto.TransformFinalBlock(plainByte, 0, plainByte.Length);
                decryptText = Regex.Replace(encoder.GetEncoded(cipherText), @"[\r\n]+", "");

                Console.WriteLine(decryptText);
                Console.WriteLine("加密结束");
            }
            return decryptText;
        }

        public static string Decrypt(string aesKey, string decryptText)
        {
            Console.WriteLine("解密开始====>");
            string plainText = "";
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(aesKey));
                var rd = sha1.ComputeHash(hash);
                byte[] keyArray = rd.Take(16).ToArray();

                byte[] decryptByte = Convert.FromBase64String(decryptText);
                RijndaelManaged AesCipher = new RijndaelManaged();
                AesCipher.KeySize = 128; // 192, 256
                AesCipher.BlockSize = 128;
                AesCipher.Mode = CipherMode.ECB;
                AesCipher.Padding = PaddingMode.PKCS7;
                //AesCipher.IV = keyArray;
                AesCipher.Key = keyArray;
                ICryptoTransform decryptor = AesCipher.CreateDecryptor();
                using (MemoryStream msDecrypt = new MemoryStream(decryptByte))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                            Console.WriteLine(plainText);
                        }
                    }
                }
                Console.WriteLine("解密结束");
            }
            return plainText;
        }
    }
}
