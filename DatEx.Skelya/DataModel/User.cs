using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

        [JsonProperty("email")]
        public String Email { get; set; }

        [JsonProperty("datasectors")]
        public List<DataSector> Datasectors { get; set; }

        [JsonProperty("roles")]
        public List<Role> Roles { get; set; }

        public override String ToString() => $"{Id} ({Name})";
    }
}
