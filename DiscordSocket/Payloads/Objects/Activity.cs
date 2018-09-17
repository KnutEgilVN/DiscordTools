using DiscordWebSocket.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordWebSocket.Payloads.Objects
{
    public class Activity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }

        [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        [JsonProperty("application_id", NullValueHandling = NullValueHandling.Ignore)]
        public string GameID { get; set; }
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
        public object Timestamps { get; set; }
    }
}
