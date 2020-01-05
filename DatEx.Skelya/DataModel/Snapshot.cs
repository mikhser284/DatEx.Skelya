using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatEx.Skelya.DataModel
{

    public class Snapshot
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("EventLogRecordId")]
        public Int32 EventLogRecordId { get; set; }

        [JsonProperty("mimetype")]
        public string MimeType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("file")]
        public string File { get; set; }
    }
}
