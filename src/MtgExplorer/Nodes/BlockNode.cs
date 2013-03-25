using Newtonsoft.Json;

namespace MtgExplorer.Nodes
{
    public class BlockNode
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}