using System.Collections.Generic;

namespace Triglav.Layers.Alexa
{
    public class AlexaCommandContent
    {
        public Dictionary<string, string> Slots { get; set; }
        public string IntentName { get; set; }
    }
}