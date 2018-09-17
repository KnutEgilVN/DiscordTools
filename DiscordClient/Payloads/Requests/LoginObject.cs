using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordClient.Payloads.Requests
{
    public class LoginObject
    {
        [JsonProperty("email")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("captcha_key")]
        public string CaptchaKey { get; set; }
        [JsonProperty("undelete")]
        public bool Undelete{ get; set; }

        public LoginObject()
        {

        }
        public LoginObject(string username, string password)
        {
            Username = username;
            Password = password;
            CaptchaKey = null;
            Undelete = false;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
