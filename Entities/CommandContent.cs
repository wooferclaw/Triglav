using System.Collections.Generic;
using Triglav.Layers;
using Triglav.Layers.Alexa;
using Triglav.Layers.Alice;
using Triglav.Layers.Telegram;

namespace Triglav.Entities
{
    public class CommandContent
    {
        public Dictionary<Locale, string> Text { get; set; }
        public string Payload { get; set; }
        public bool IsEnter { get; set; }
        public AliceCommandContent AliceCommandContent { get; set; }
        public TelegramCommandContent TelegramCommandContent { get; set; }
        public AlexaCommandContent AlexaCommandContent { get; set; }
    }
}