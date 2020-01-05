using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class Event
    {
        /// <summary> Unique event guid </summary>
        [JsonProperty("id")]
        public String Id { get; set; }

        /// <summary> The name of Event </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary> The type of Event </summary>
        [JsonProperty("type")]
        public String Type { get; set; }

        /// <summary> Event related triggers </summary>
        [JsonProperty("triggers")]
        public List<Trigger> Triggers { get; set; }

        [JsonProperty("device_type")]
        public String DeviceType { get; set; }

        public override string ToString() => Name;
    }
}
