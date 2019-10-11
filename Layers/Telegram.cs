namespace Triglav.Layers
{
    public class Telegram
    {
        public string Command { get; set; }
        public string Payload { get; set; }
        public bool IsLocation { get; set; }
        public bool IsImage { get; set; }
    }
}