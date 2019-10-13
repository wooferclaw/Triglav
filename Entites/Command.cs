using Triglav.Layers;

namespace Triglav
{
    public class Command
    {
        public User User { get; set; }
        public string Text { get; set; }
        public string Payload { get; set; }

        public AliceCommandContent AliceCommand { get; set; }
        public TelegramCommandContent TelegramCommand { get; set; }
        //for each layer takes json and makes request
        public string From(Engine.Layer layer)
        {
            return "";
        }
    }
}