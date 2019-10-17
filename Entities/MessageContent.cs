using System.Collections.Generic;
using Triglav.Layers;

namespace Triglav.Entities
{
    public class MessageContent
    {
        public string Text { get; set; }

        public string Picture { get; set; }

        public List<string> Buttons { get; set; }

        private AliceMessageContent AliceMessage {get;set;}
        private TelegramMessageContent TelegramMessage { get; set; }

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