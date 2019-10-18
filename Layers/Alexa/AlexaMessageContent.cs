namespace Triglav.Layers.Alexa
{
    public class AlexaMessageContent
    {
        public string Ssml { get; set; }
        public bool Reprompt { get; set; }
        public bool ShouldEndSession { get; set; }
    }
}