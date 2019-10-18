using System.Collections.Generic;
using Triglav.Layers;

namespace Triglav.Entities
{
    public class MessageContent
    {
        public Dictionary<Locale, string> Text { get; set; }
        public Dictionary<Locale, string[]> Buttons { get; set; }
        public bool InlineButtons { get; set; }

        public AliceMessageContent AliceMessageContent { get; set; }
        public TelegramMessageContent TelegramMessageContent { get; set; }
    }
}