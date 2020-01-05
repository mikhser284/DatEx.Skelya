using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class EventLogRecord
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("datetime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("event")]
        public Event Event { get; set; }

        [JsonProperty("devices")]
        public List<Device> Devices { get; set; }

        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty("data")]
        public EventLogRecordData Data { get; set; }
    }
}
