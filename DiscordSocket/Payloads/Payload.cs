using DiscordWebSocket.Enums;
using DiscordWebSocket.Interfaces;
using DiscordWebSocket.Payloads.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordWebSocket.Payloads
{
    public class Payload
    {
        [JsonProperty("op")]
        public int Opcode { get; set; }
        [JsonProperty("d")]
        public object Data { get; set; }
        //[JsonProperty("s")]
        //public int Sequence
        //{
        //    get
        //    {
        //        return _Sequence;
        //    }
        //    set
        //    {
        //        _Sequence = value;
        //        LastSequence = _Sequence;
        //    }
        //}
        //[JsonProperty("t")]
        //public string EventName { get; set; }

        private int _Sequence;
        public static int LastSequence { get; set; }

        public Payload()
        {

        }
        public Payload(Opcodes opcode, object data)
        {
            Opcode = (int)opcode;
            Data = data;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
