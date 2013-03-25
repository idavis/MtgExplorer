using MtgExplorer.Mtg;
using Newtonsoft.Json;

namespace MtgExplorer.Entities
{
    public class Cost
    {
        [JsonProperty("ManaCost")]
        public string ManaCost { get; set; }

        [JsonProperty("ConvertedManaCost")]
        public string ConvertedManaCost { get; set; }

        [JsonProperty("Colors")]
        public Color[] Colors { get; set; }
    }
}