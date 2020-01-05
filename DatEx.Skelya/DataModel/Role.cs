using System;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class Role
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public override String ToString() => Name;
    }

}
