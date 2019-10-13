using Triglav.Layers;

namespace Triglav
{
    public class CommandContent
    {
        public string Text { get; set; }
        public string Payload { get; set; }
        //Constructor?
        public AliceCommandContent AliceCommand { get; set; }
        public TelegramCommandContent TelegramCommand { get; set; }

        public void For(AliceCommandContent command)
        {
            AliceCommand = command;
        }
        public void For(TelegramCommandContent command)
        {
            TelegramCommand = command;
        }
    }
}