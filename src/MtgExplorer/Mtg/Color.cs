using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MtgExplorer.Mtg
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Color
    {
        None,
        Black,
        Blue,
        Green,
        Red,
        White,
        Phrexian,
        Colorless
    }
}