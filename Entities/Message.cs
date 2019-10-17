using System.Collections.Generic;

namespace Triglav.Entities
{
    public class Message
    {
        public string Text { get; set; }
        public string Picture { get; set; }
        public List<string> Buttons { get; set; }

        public Message(MessageContent messageContent, User user)
        {
            
        }

        //convert to aliceresponce or telegramresponce (take similar fields and return them as necessary)
        public string As(Engine.Layer layer)
        {
            return "";
        }
    }
}