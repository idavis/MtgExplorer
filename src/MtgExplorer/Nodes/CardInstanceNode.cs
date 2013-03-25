using Newtonsoft.Json;

namespace MtgExplorer.Nodes
{
    public class CardInstanceNode : CardNode
    {
        [JsonProperty("MultiverseId")]
        public string MultiverseId { get; set; }

        [JsonProperty("CardNumber")]
        public string CardNumber { get; set; }

        [JsonProperty("Artist")]
        public string Artist { get; set; }

        [JsonProperty("Rarity")]
        public string Rarity { get; set; }

        [JsonProperty("Power")]
        public string Power { get; set; }

        [JsonProperty("Toughness")]
        public string Toughness { get; set; }

        [JsonProperty("LoyaltyCounters")]
        public string LoyaltyCounters { get; set; }
    }
}