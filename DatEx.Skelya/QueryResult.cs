using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using DatEx.Skelya.DataModel;
using System;

namespace DatEx.Skelya
{

    public class QueryResult <T>
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("success")]
        public Boolean? IsSuccess { get; set; }

        [JsonProperty("message")]
        public String Message { get; set; }
    }

    public class IdentifiedEvent
    {
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        [JsonProperty("event")]
        public Event Event { get; set; }
    }

    public class IdentifiedDevice
    {
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }
    }
}
