using DiscordWebSocket.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordWebSocket.Payloads.Objects
{
    public class StatusUpdate
    {
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("game"/*, NullValueHandling = NullValueHandling.Ignore*/)]
        public Activity Game { get; set; }
        [JsonProperty("afk")]
        public bool AFK { get; set; }
        [JsonProperty("since"/*, NullValueHandling = NullValueHandling.Ignore*/)]
        public int AFKSince { get; set; }
    }
}
