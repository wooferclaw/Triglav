using System.Reflection.Metadata.Ecma335;
using Triglav.Layers;

namespace Triglav
{
    public class MessageContent
    {
        public string Text { get; set; }

        public string Picture { get; set; }

        public AliceMessageContent AliceMessage {get;set;}
        public TelegramMessageContent TelegramMessage { get; set; }

        public void For(AliceMessageContent content)
        {
            AliceMessage = content;
        }
        public void For(TelegramMessageContent content)
        {
            TelegramMessage = content;
        }
    }
}