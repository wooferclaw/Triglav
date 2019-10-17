namespace Triglav.Layers
{
    public class TelegramMessageContent
    {
        public string ParseMode { get; set; } = "HTML";
        public int[] ButtonsByRows { get; set; }
        public bool OneTimeKeyboard { get; set; } = true;
    }
}