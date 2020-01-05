using System;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class Trigger
    {
        /// <summary> The ID of Trigger </summary>
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        [JsonProperty("event_id")]
        /// <summary> Unique event guid </summary>
        public String EventId { get; set; }

        /// <summary> The name of trigger </summary>
        [JsonProperty("name")]
        public String Name { get; set; }

        /// <summary> The value of trigger </summary>
        [JsonProperty("value")]
        public Int64 Value { get; set; }

        public override string ToString() => Name;
    }
}
