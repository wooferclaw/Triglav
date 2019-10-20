using Triglav.Entities;

namespace Triglav.Layers.Alexa
{
    public class AlexaMessageContent
    {
        public string Ssml { get; set; }
        public MessageContent Reprompt { get; set; }
        public bool ShouldEndSession { get; set; }
    }
}