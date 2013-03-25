using MtgExplorer.Mtg;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MtgExplorer.Entities
{
    public class CardInstance : Card
    {
        [JsonProperty("PrintedCardText")]
        public string PrintedCardText { get; set; }

        [JsonProperty("OracleCardText")]
        public string[] OracleCardText { get; set; }

        [JsonProperty("ReminderText")]
        public string ReminderText { get; set; }

        [JsonProperty("FlavorText")]
        public string FlavorText { get; set; }

        [JsonProperty("AbilityWords")]
        public string[] AbilityWords { get; set; }

        [JsonProperty("Set")]
        public string Set { get; set; }

        [JsonProperty("MultiverseId")]
        public string MultiverseId { get; set; }

        [JsonProperty("Watermark")]
        [JsonConverter(typeof (StringEnumConverter))]
        public Watermark Watermark { get; set; }

        [JsonProperty("CardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("Artist")]
        public string Artist { get; set; }

        // TODO: Populate these connections in the DB
        //[JsonProperty("Block")]
        //public Block Block { get; set; }

        [JsonProperty("Rarity")]
        public string Rarity { get; set; }

        [JsonProperty("PowerToughness")]
        public string PowerToughness { get; set; }
    }
}