using System;
using Triglav.Entities;

namespace Triglav
{
    public class Engine
    {
        public enum Layer
        {
            Alice,
            Alexa,
            Telegram,
            VK,
            Facebook
        }

        public Engine(Layer[] layers)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Command ParseCommand(string jsonBody, Layer layer)
        {

            throw new NotImplementedException();
        }
    }
}
