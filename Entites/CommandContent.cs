using Triglav.Layers;

namespace Triglav
{
    public class CommandContent
    {
        public string Command { get; set; }
        public string Payload { get; set; }

        //И хранить его где-то внутри.А конструктор с полями которые одинаковы везде (текст, кнопки)
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