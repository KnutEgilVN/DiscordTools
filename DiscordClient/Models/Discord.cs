using DiscordClient.Controllers;
using DiscordClient.Payloads.Requests;
using DiscordClient.Payloads.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordClient.Models
{
    public class Discord
    {
        public Discord()
        {

        }

        public string Login(string username, string password)
        {
            string json = SocketController.Post(DiscordURL.Login, new LoginObject(username, password));
            LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(json);
            return response.Token;
        }
    }

    public class DiscordURL
    {
        public static string Login = "https://discordapp.com/api/v6/auth/login";
    }
}
