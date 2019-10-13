namespace Triglav
{
    public class Command
    {
        public string Text { get; set; }
        public string Payload { get; set; }
        
        //for each layer takes json and makes request
        public string From(Engine.Layer layer)
        {
            return "";
        }
    }
}