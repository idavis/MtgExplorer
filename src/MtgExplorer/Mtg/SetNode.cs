using System;
using Newtonsoft.Json;

namespace MtgExplorer.Mtg
{
    public class SetNode
    {
        public SetNode()
        {
            TotalCards = -1;
            Common = -1;
            Uncommon = -1;
            Rare = -1;
            MythicRare = -1;
            BasicLand = -1;
            TotalCards = -1;
        }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ReleaseDate")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("PreReleaseDate")]
        public DateTime PreReleaseDate { get; set; }

        [JsonProperty("ExpansionCode")]
        public string ExpansionCode { get; set; }

        [JsonProperty("TotalCards")]
        public int TotalCards { get; set; }

        [JsonProperty("Common")]
        public int Common { get; set; }

        [JsonProperty("Uncommon")]
        public int Uncommon { get; set; }

        [JsonProperty("Rare")]
        public int Rare { get; set; }

        [JsonProperty("MythicRare")]
        public int MythicRare { get; set; }

        [JsonProperty("BasicLand")]
        public int BasicLand { get; set; }

        [JsonProperty("Other")]
        public int Other { get; set; }
    }
}