using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using DiscordClient.Payloads.Requests;

namespace DiscordClient.Controllers
{
    public class SocketController
    {
        public static WebClient WebClient
        {
            get
            {
                return new WebClient()
                {
                    Headers = WebHeaders
                };
            }
        }
        public static WebHeaderCollection WebHeaders
        {
            get
            {
                return new WebHeaderCollection()
                {
                    { "content-type", "application/json" }
                };
            }
        }
        private static JsonSerializerSettings _Settings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
            }
        }

        public static string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, _Settings);
        }

        public static string Post(string url, object data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data, _Settings);

                return WebClient.UploadString(url, json);
            }
            catch(WebException ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }
    }
}
