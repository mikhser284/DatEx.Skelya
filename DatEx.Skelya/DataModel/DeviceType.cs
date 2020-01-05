using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{

    public class DeviceType
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("classifier")]
        public string Classifier { get; set; }

        public override String ToString() => $"{Name} ({Classifier})";
    }
}
