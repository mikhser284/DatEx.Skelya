using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class Device
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public DeviceType Type { get; set; }

        [JsonProperty("datasector")]
        public DataSector DataSector { get; set; }

        public override System.String ToString() => $"{Name}\n   - Type:       {Type}\n   - DataSector: {DataSector}";
    }
}
