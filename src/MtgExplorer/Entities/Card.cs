using MtgExplorer.Mtg;
using Newtonsoft.Json;

namespace MtgExplorer.Entities
{
    public class Card
    {
        /// <summary>
        ///     Two objects having the same name in the English versions of their names are identical
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Cost")]
        public Cost Cost { get; set; }

        [JsonProperty("Types")]
        public Types[] Types { get; set; }

        // ArtifactTypes, EnchantmentTypes, LandTypes, PlaneswalkerTypes, SpellTypes, CreatureTypes, PlanarTypes
        [JsonProperty("Subtypes")]
        public string[] Subtypes { get; set; }

        //[JsonProperty("Supertypes")]
        //public Supertypes[] Supertypes { get; set; }

        [JsonProperty("Sets")]
        public string[] Sets { get; set; }

        //[JsonProperty("Blocks")]
        //public Block[] Blocks { get; set; }

        [JsonProperty("Restrictions")]
        public Restriction[] Restrictions { get; set; }

        [JsonProperty("Rulings")]
        public Ruling[] Rulings { get; set; }
    }
}