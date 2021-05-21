using Newtonsoft.Json;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string publicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCDBXsLxY4Xs6ClnvIZRJYzoLYPKPRBO5/LQpYC
4PeYXrR4i3OOQvTJ0wTj1u7AQSKS378pzPPfFJ/phWa8Q9Dh35cN2eM788X8jZ2Z6nJOH+sBpWjk
OML4oVGU5bknooCYYzkTRvJeZvMxeaOoYntKPCLZWj4RTV5ArLXrLqTPcwIDAQAB";
            ReqModel root = new ReqModel();
            root.data = new Data();

            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0, 0));
            long timestamp = (DateTime.Now.Ticks - startTime.Ticks) / 10000;
            root.timestamp = timestamp.ToString();
            string aesKey = root.version + root.customId + root.apiId + root.timestamp + publicKey.Split('\r')[1].Substring(1, 48);
            Console.WriteLine("aes key====>");
            Console.WriteLine(aesKey);
            Console.WriteLine("aes key结束");

            string plianText = JsonConvert.SerializeObject(root.data);

            Console.WriteLine("请求参数====>");
            Console.WriteLine(plianText);
            Console.WriteLine("请求参数结束");

            string decryptText = AesHelper.Encrypt(aesKey, plianText);


            string sign = RSAHelper.RSAEncry(aesKey, publicKey);
            string response = HttpHelper.DoPost(root, decryptText, sign);

            Console.WriteLine("响应参数====>");
            Console.WriteLine(response);
            Console.WriteLine("响应参数结束");

            ResModel res = JsonConvert.DeserializeObject<ResModel>(response);
            aesKey = res.version + res.customId + res.apiId + res.timestamp + publicKey.Split('\r')[1].Substring(1, 48);

            Console.WriteLine("aes key====>");
            Console.WriteLine(aesKey);
            Console.WriteLine("aes key结束");
            if (RSAHelper.SignCheck(res.sign, publicKey))
                plianText = AesHelper.Decrypt(aesKey, res.data);
            Console.ReadLine();
        }
    }
}
