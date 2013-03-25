using Newtonsoft.Json;

namespace MtgExplorer.Nodes
{
    public class CardNode
    {
        private string[] _abilityWords;
        private string[] _oracleCardText;

        /// <summary>
        /// Two objects having the same name in the English versions of their names are identical
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("PrintedCardText")]
        public string PrintedCardText { get; set; }

        [JsonProperty("OracleCardText")]
        public string[] OracleCardText
        {
            get { return _oracleCardText; }
            set
            {
                if (value != null && value.Length == 0)
                {
                    _oracleCardText = null;
                }
                else
                {
                    _oracleCardText = value;
                }
            }
        }

        [JsonProperty("ReminderText")]
        public string ReminderText { get; set; }

        [JsonProperty("FlavorText")]
        public string FlavorText { get; set; }

        [JsonProperty("AbilityWords")]
        public string[] AbilityWords
        {
            get { return _abilityWords; }
            set
            {
                if (value != null && value.Length == 0)
                {
                    _abilityWords = null;
                }
                else
                {
                    _abilityWords = value;
                }
            }
        }
    }
}