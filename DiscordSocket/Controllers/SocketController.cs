using DiscordWebSocket.Payloads;
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

namespace DiscordWebSocket.Controllers
{
    public class SocketController
    {
        public static bool IsConnected
        {
            get
            {
                return WebSocket.State == WebSocketState.Open;
            }
        }
        public static ClientWebSocket WebSocket
        {
            get
            {
                if (_WebSocket == null)
                    _WebSocket = new ClientWebSocket();

                return _WebSocket;
            }
        }
        private static ClientWebSocket _WebSocket;
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

        public static bool Connect(string url)
        {
            WebSocket.ConnectAsync(new Uri(url), 
                new CancellationToken()).GetAwaiter().GetResult();

            return WebSocket.State == WebSocketState.Open;
        }
        public static void Send(Payload payload)
        {
            while (WebSocket.State != WebSocketState.Open)
                Thread.Sleep(1);

            string json = Serialize(payload);
            byte[] buffer = Encoding.UTF8.GetBytes(json);

            WebSocket.SendAsync(new ArraySegment<byte>(buffer),
                WebSocketMessageType.Text,
                true,
                new CancellationToken());
        }
        public static Gateway Receive()
        {
            while (WebSocket.State != WebSocketState.Open)
                Thread.Sleep(1);

            byte[] buffer = new byte[4096];
            WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), 
                new CancellationToken());
            
            int lastIndex = buffer.ToList().FindLastIndex(b => b != 0);
            byte[] tempBuffer = new byte[lastIndex+1];
            Buffer.BlockCopy(buffer, 0, tempBuffer, 0, tempBuffer.Length);

            string json = Encoding.UTF8.GetString(tempBuffer);
            Gateway payload = Deserialize<Gateway>(json);

            return payload;
        }

        public static void Send(string json)
        {
            WebSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(json)), WebSocketMessageType.Text, true, new CancellationToken()).GetAwaiter().GetResult();
        }
        public static string ReceiveString()
        {
            byte[] buffer = new byte[4096];

            WebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), new CancellationToken()).GetAwaiter().GetResult();
            
            int lastIndex = buffer.ToList().FindLastIndex(b => b != 0);
            byte[] tempBuffer = new byte[lastIndex + 1];
            Buffer.BlockCopy(buffer, 0, tempBuffer, 0, tempBuffer.Length);

            string json = Encoding.UTF8.GetString(tempBuffer);
            return json;
        }
    }
}
