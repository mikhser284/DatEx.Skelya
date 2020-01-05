using System;
using Newtonsoft.Json;

namespace DatEx.Skelya.DataModel
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
