using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MtgExplorer.Mtg
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Types
    {
        Artifact,
        Basic,
        Creature,
        Enchantment,
        Instant,
        Land,
        Legendary,
        Ongoing,
        Phenomenon,
        Plane,
        Planeswalker,
        Scheme,
        Snow,
        Sorcery,
        Tribal,
        Vanguard,
        World
    }
}