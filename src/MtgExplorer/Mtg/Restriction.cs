using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MtgExplorer.Mtg
{
    public class Restriction
    {
        [JsonProperty("Format")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Format Format { get; set; }

        [JsonProperty("Legality")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Legality Legality { get; set; }
    }
}