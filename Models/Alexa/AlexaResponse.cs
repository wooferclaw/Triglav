namespace Triglav.Models.Alexa
{
    public class SessionAttributes
    {
        public string Key { get; set; }
    }

    public class OutputSpeech
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public string SSML { get; set; }
        public string PlayBehavior { get; set; }
    }

    public class Reprompt
    {
        public OutputSpeech OutputSpeech { get; set; }
    }

    public class Response
    {
        public OutputSpeech OutputSpeech { get; set; }
        public Reprompt Reprompt { get; set; }
        public bool ShouldEndSession { get; set; }
    }

    public class AlexaResponse
    {
        public string Version { get; set; }
        public SessionAttributes SessionAttributes { get; set; }
        public Response Response { get; set; }
    }
}