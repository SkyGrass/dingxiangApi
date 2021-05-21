using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace TestApp
{
    public class RSAHelper
    {
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="content">加密明文</param>
        /// <param name="rsaKey">公钥</param>
        /// <returns>返回密文</returns>
        public static string RSAEncry(string content, string rsaKey)
        {
            Console.WriteLine("签名开始====>");
            AsymmetricKeyParameter key = PublicKeyFactory.CreateKey(Convert.FromBase64String(rsaKey));
            IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            c.Init(true, key);
            byte[] byteData = Encoding.UTF8.GetBytes(content);
            byteData = c.DoFinal(byteData, 0, byteData.Length);
            string result = Convert.ToBase64String(byteData);
            Console.WriteLine(result);
            Console.WriteLine("签名结束");
            return result;
        }

        /// <summary>
        /// 未实现
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static bool SignCheck(string sign, string publicKey)
        {
            return true;
        }
    }
}
