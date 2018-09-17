using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DiscordClient.Models;
using DiscordWebSocket.Controllers;
using DiscordWebSocket.Enums;
using DiscordWebSocket.Models;
using DiscordWebSocket.Payloads;
using DiscordWebSocket.Payloads.Objects;

namespace DiscordConsole
{
    class Program
    {
        static void Main(string[] args)
            => new Program();

        public Program()
        {
            Start();
            Console.ReadLine();
        }

        public void Start()
        {
            Discord client = new Discord();
            DiscordSocket sock = new DiscordSocket();

            string token = client.Login("ninjasploit+discord@gmail.com", "Figaro123");

            Console.WriteLine($"Connected: {sock.Connect()}");
            Console.WriteLine($"Is Listening: {sock.SocketListener.IsListening}");
            Console.WriteLine($"Is Heartbeat: {sock.SocketHeart.DoHeartbeat}");

            sock.DoIdentify(token, new ConnectionProperties()
            {
                OS = "Windows",
                Browser = "Chrome",
                Device = ""
            });

            Console.ReadLine();
            sock.Send(new Payload(Opcodes.StatusUpdate, 
                new StatusUpdate()
                {
                    Status = StatusTypes.Online,
                    AFK = false
                }).ToString());
            Console.WriteLine(sock.Receive());
            Console.ReadLine();
            //Console.WriteLine(sock.Receive());

            /*sock.DoIdentify("MjAzNTI2OTQ3NjAxMzgzNDI0.DnraoA.FwO0moTyQljYWI8audSVMLQTXv4", new ConnectionProperties()
            {
                OS = "Windows",
                Browser = "App",
                Device = "Desktop"
            });*/
        }
    }
}
