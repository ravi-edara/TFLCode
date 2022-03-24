using System.Net;
using Newtonsoft.Json;

namespace TFLCodeChallenge.Tests.Acceptance
{
    public class WebRequest
    {
        public static string CreateWebRequest(string url, string method = "GET")
        {
            var attempt = 1;

            do
            {
                try
                {
                    var content = CreateWebRequestInternal(attempt, url, method);

                    if (content == "Unable to connect to the remote server" || content == "The operation has timed out")
                        throw new TimeoutException();

                    return content;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(10000);
                }

                attempt++;
            } while (attempt <= 3);

            throw new Exception("Request failed");
        }

        public static string CreateWebRequestInternal(int attempt, string url, string method)
        {
            return CreateWebRequestInternal<string>(attempt, url, method, null);
        }

        public static string CreateWebRequestInternal<TBody>(int attempt, string url, string method, TBody body)
        {
            HttpWebResponse response = null;

            try
            {
                var webRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);

                if ((method.ToLower() == "post") || (method.ToLower() == "put"))
                    webRequest.ContentType = "application/json";
                webRequest.Timeout = 30000;
                webRequest.Method = method;
                Console.WriteLine($"(Attempt {attempt}) Sending {webRequest.Method} request to: {url}");

                if (body == null && (method.ToLower() == "post" || method.ToLower() == "put"))
                {
                    Console.WriteLine("Getting request stream");
                    using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        Console.WriteLine("Setting empty body");
                        streamWriter.Write("");
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }
                else if (body != null)
                {
                    Console.WriteLine("Getting request stream");
                    using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                    {
                        Console.WriteLine("Serializing body");
                        var jsonData = JsonConvert.SerializeObject(body);
                        Console.WriteLine($"Sending body {jsonData} to: ${url}");
                        streamWriter.Write(jsonData);
                        streamWriter.Flush();
                        streamWriter.Close();
                    }
                }

                response = (HttpWebResponse)webRequest.GetResponse();
                Console.WriteLine($"Response is {response.StatusCode} from: {url}");

                return response.AsString();
            }
            catch (WebException exception)
            {
                Console.WriteLine(exception.Message);
                response = (HttpWebResponse)exception.Response;
                if (response != null)
                {
                    var content = response.AsString();
                    Console.WriteLine($"Response is {response.StatusCode} : {content} from: {url}");

                    var c = (int)response.StatusCode;
                    if (c >= 300 && c < 500)
                        return content;
                }

                throw;
            }
            finally
            {
                response?.Close();
            }
        }
    }
}