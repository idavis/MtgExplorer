using Newtonsoft.Json;

namespace MtgExplorer.Nodes
{
    public abstract class AbilityNode
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}