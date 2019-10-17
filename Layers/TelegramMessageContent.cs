using System.Collections.Generic;

namespace Triglav.Layers
{
    public class TelegramMessageContent
    {
        public string ParseMode { get; set; } = "HTML";
        public int[] ButtonsByRows { get; set; }

    }
}