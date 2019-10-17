using System.Collections.Generic;

namespace Triglav.Layers
{
    public class TelegramMessageContent
    {
        public string ParseMode { get; set; }
        public int[] ButtonsByRows { get; set; }
    }
}