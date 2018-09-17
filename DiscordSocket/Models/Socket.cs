﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using DiscordWebSocket.Interfaces;
using DiscordWebSocket.Payloads;
using Newtonsoft.Json;
using DiscordWebSocket.Controllers;
using DiscordWebSocket.Enums;
using DiscordWebSocket.Payloads.Objects;

namespace DiscordWebSocket.Models
{
    public class DiscordSocket
    {
        public bool IsConnected { get; set; }
        public SocketListener SocketListener { get; set; }
        public SocketHeart SocketHeart { get; set; }

        private static JsonSerializerSettings _Settings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    //NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
            }
        }

        public DiscordSocket()
        {
            SocketListener = new SocketListener();
            SocketHeart = new SocketHeart();

            SocketListener.GatewayReceived += (p) =>
            {
                //Console.WriteLine(p);

                switch ((Opcodes)p.Opcode)
                {
                    case Opcodes.HeartbeatACK:
                        Console.WriteLine("Heartbeat ACK");
                        break;

                    case Opcodes.Heartbeat:
                        SocketHeart.Beat();
                        break;

                    case Opcodes.Hello:
                        Console.WriteLine("Hello Received");
                        Hello hello = Deserialize<Hello>(p.Data.ToString());
                        SocketHeart.HeartbeatInterval = hello.HeartbeatInterval;
                        SocketHeart.Start();
                        break;

                    default:
                        //Console.WriteLine("Gateway received, No actions taken");
                        Console.WriteLine(p);
                        break;
                }
            };
            SocketHeart.HeartStarted += () =>
            {
                Console.WriteLine("Heart Started");
            };
            SocketHeart.HeartbeatSent += (a) =>
            {
                if (a == false)
                    Console.WriteLine($"Something went wrong, heartbeat not acknowledged!");
            };
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public T Deserialize<T>(object data)
        {
            return JsonConvert.DeserializeObject<T>(data.ToString(), _Settings);
        }

        public bool Connect()
        {
            bool connected = SocketController.Connect("wss://gateway.discord.gg/?v=6&encoding=json");
            SocketListener.Listen();

            return connected;
        }
        public void DoIdentify(string token, ConnectionProperties properties, bool compress = false, int large_threshold = 50, int[] shard = null, object presence = null)
        {
            Identify identify = new Identify()
            {
                Token = token,
                Properties = properties
                //Compress = compress,
                //Large_threshold = large_threshold,
                //Shard = shard,
                //Presence = presence
            };
            Payload payload = new Payload(Opcodes.Identify, identify);
            Send(payload.ToString());
        }
        public void Send(string json)
        {
            SocketController.Send(json);
        }
        public string Receive()
        {
            return SocketController.ReceiveString();
        }
     }
}
