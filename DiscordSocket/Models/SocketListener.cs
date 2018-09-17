using DiscordWebSocket.Controllers;
using DiscordWebSocket.Payloads;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordWebSocket.Models
{
    public class SocketListener
    {
        public bool IsListening { get; set; }

        public delegate void GatewayReceivedCallback(Gateway gateway);
        public event GatewayReceivedCallback GatewayReceived;
        private string _TemporaryJSON { get; set; }

        public SocketListener()
        {
            _TemporaryJSON = "";
        }

        public void Listen()
        {
            IsListening = true;

            new Thread(() =>
            {
                while (IsListening)
                {
                    if (SocketController.IsConnected)
                    {
                        string json = SocketController.ReceiveString();
                        _TemporaryJSON += json;

                        try
                        {
                            Gateway gateway = JsonConvert.DeserializeObject<Gateway>(_TemporaryJSON, new JsonSerializerSettings()
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                MissingMemberHandling = MissingMemberHandling.Ignore
                            });
                            //Console.WriteLine(SocketController.ReceiveString()); 
                            if (gateway != null)
                                GatewayReceived(gateway);

                            _TemporaryJSON = "";
                        }
                        catch(JsonReaderException jre)
                        {

                        }
                        catch(JsonWriterException jwe)
                        {

                        }
                        catch(JsonSerializationException jse)
                        {

                        }
                    }
                }
            }).Start();
        }
        public void Stop()
        {
            IsListening = false;
        }
    }
}
