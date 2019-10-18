using System.Collections.Generic;
using Triglav.Layers;

namespace Triglav.Entities
{
    public class CommandContent
    {
        public Dictionary<Locale, string> Text { get; set; }
        public string Payload { get; set; }
        public bool IsEnter { get; set; }
        public AliceCommandContent AliceCommandContent { get; set; }
        public TelegramCommandContent TelegramCommandContent { get; set; }
    }
}