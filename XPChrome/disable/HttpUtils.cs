using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace XPChrome
{
    public class HttpUtils
    {
        public static string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);//(HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            //request.Headers.Add("token", str_token);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream myResponseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.UTF8))
                    {
                        string result = myStreamReader.ReadToEnd();
                        return result;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool PostForm(string requestUri, Dictionary<string, string> formData)
        {
            var expiration = DateTime.Now.AddMinutes(10);
            var boundary = Guid.NewGuid().GetHashCode().ToString("X2");
            var webRequest = (HttpWebRequest)WebRequest.Create(requestUri);
            webRequest.Timeout = -1;
            webRequest.Method = "POST";
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Accept = "application/json";
            var reqBody = new StringBuilder();
            reqBody.Append("--" + boundary + "\r\n");
            var ie = formData.GetEnumerator();
            while (ie.MoveNext())
            {
                reqBody.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n", ie.Current.Key);
                reqBody.Append("\r\n" + ie.Current.Value + "\r\n");
                reqBody.Append("--" + boundary + "\r\n");
            }
            //reqBody.Append("Content-Disposition: form-data; name=\"submit\"\r\n\r\nOSSInit\r\n");
            reqBody.Append("--" + boundary + "--\r\n");
            webRequest.ContentLength = reqBody.Length;
            //webRequest.Headers.Add("token", System.Configuration.ConfigurationManager.AppSettings["token"].ToString());
            using (var ms = new MemoryStream())
            {
                var writer = new StreamWriter(ms, new UTF8Encoding());
                try
                {
                    writer.Write(reqBody.ToString());
                    writer.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    webRequest.ContentLength = ms.Length;
                    using (var requestStream = webRequest.GetRequestStream())
                    {
                        ms.WriteTo(requestStream);
                    }
                }
                finally
                {
                    writer.Dispose();
                }
            }
            var txt = string.Empty;
            var response = webRequest.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var sr = response.GetResponseStream();
                using (var strm = new StreamReader(sr))
                {
                    txt = strm.ReadToEnd();
                    strm.Close();
                }
            }
            return true;
        }
    }
}