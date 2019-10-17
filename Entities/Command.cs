using Triglav.Layers;

namespace Triglav.Entities
{
    public class Command
    {
        public User User { get; set; }
        public string Text { get; set; }
        public string Payload { get; set; }

        public AliceCommand AliceCommand { get; set; }
        public TelegramCommand TelegramCommand { get; set; }
        //for each layer takes json and makes request
        public Command From(Engine.Layer layer, string body)
        {
            return this;
        }
    }
}