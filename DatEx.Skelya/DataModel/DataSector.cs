using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class DataSector
    {
        /// <summary> Id of datasector </summary>
        [JsonProperty("id")]
        public Int32 Id { get; set; }

        /// <summary> The name of data sector. It can be an organization </summary>
        [JsonProperty("name")]
        public String Name { get; set; }

        public override String ToString() => Name;
    }
}
