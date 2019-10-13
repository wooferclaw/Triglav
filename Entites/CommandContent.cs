using Triglav.Layers;

namespace Triglav
{
    public class CommandContent
    {
        public string Command { get; set; }
        public string Payload { get; set; }

        //Constructor?
        public AliceCommandContent AliceCommand { get; set; }
        public TelegramCommandContent TelegramCommand { get; set; }

        public void For(AliceCommandContent data)
        {
            
        }
        public void For(TelegramCommandContent data)
        {

        }
        
    }
}