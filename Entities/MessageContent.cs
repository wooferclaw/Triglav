using System.Collections.Generic;
using Triglav.Layers;

namespace Triglav.Entities
{
    public class MessageContent
    {
        public string Text { get; set; }
        public string Picture { get; set; }
        public string[] Buttons { get; set; }
        public bool InlineButtons { get; set; }


        public AliceMessageContent AliceMessageContent {get;set;}
        public TelegramMessageContent TelegramMessageContent { get; set; }

        public void For(AliceMessageContent content)
        {
            AliceMessageContent = content;
        }
        public void For(TelegramMessageContent content)
        {
            TelegramMessageContent = content;
        }
    }
}