namespace Triglav
{
    public class Command
    {
        public string Text { get; set; }
        public string Payload { get; set; }
        public string From(Engine.Layer layer)
        {
            return "";
        }
    }
}