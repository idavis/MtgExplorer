using System;
using Newtonsoft.Json;

namespace MtgExplorer.Mtg
{
    public class Ruling
    {
        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [JsonProperty("RulingText")]
        public string RulingText { get; set; }
    }
}