using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public static class HttpHelper
    {
        public static string DoPost(ReqModel root, string decryptTxt, string sign)
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://dxcare.cn/manage/openapi/handle");
            req.Method = "POST";
            req.ContentType = "application/json";
            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new
            {
                apiId = root.apiId,
                customId = root.customId,
                timestamp = root.timestamp,
                version = root.version,
                data = decryptTxt,
                sign = sign
            }));
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
