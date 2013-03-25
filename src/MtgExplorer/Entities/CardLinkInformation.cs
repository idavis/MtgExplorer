using Newtonsoft.Json;

namespace MtgExplorer.Entities
{
    public class CardLinkInformation
    {
        private const string _UriPrefix = "http://gatherer.wizards.com";
        private const string _CardUri = _UriPrefix + "/Pages/Card/Details.aspx?multiverseid={0}";
        private const string _ImageUri = _UriPrefix + "/Handlers/Image.ashx?multiverseid={0}&type=card";

        // for serialization
        public CardLinkInformation()
        {
        }

        public CardLinkInformation(string multiversId, string name, string set)
        {
            MultiverseId = multiversId;
            Name = name;
            Set = set;
            Reinitialize();
        }

        [JsonProperty("MultiverseId")]
        public string MultiverseId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Set")]
        public string Set { get; set; }

        [JsonProperty("CardUri")]
        public string CardUri { get; set; }

        [JsonProperty("ImageUri")]
        public string ImageUri { get; set; }

        public void Reinitialize()
        {
            CardUri = string.Format(_CardUri, MultiverseId);
            ImageUri = string.Format(_ImageUri, MultiverseId);
        }
    }
}