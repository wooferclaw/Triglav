using Triglav.Layers;

namespace Triglav.Entities
{
    public class CommandContent
    {
        public string Text { get; set; }
        public string Payload { get; set; }
        //Constructor?
        public AliceCommandContent AliceCommandContent { get; set; }
        public TelegramCommandContent TelegramCommandContent { get; set; }

        public void For(AliceCommandContent command)
        {
            AliceCommandContent = command;
        }
        public void For(TelegramCommandContent command)
        {
            TelegramCommandContent = command;
        }
    }
}