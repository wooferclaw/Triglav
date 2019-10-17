using Triglav.Models;
using Message = Triglav.Models.Message;

namespace Triglav.Layers
{
    public class TelegramCommand
    {
        public TelegramCommand(Message message)
        {

        }

        public TelegramCommand(CallbackQuery query)
        {

        }
    }
}