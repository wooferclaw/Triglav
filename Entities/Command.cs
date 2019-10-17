using System;
using System.Data;
using Newtonsoft.Json;
using Triglav.Layers;
using Triglav.Models;

namespace Triglav.Entities
{
    public class Command
    {
        public string Id { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string Payload { get; set; }

        public AliceCommand AliceCommand { get; set; }
        public TelegramCommand TelegramCommand { get; set; }
        

        public Command From(Engine.Layer layer, string body)
        {
            return layer switch
            {
                Engine.Layer.Telegram => FromTelegram(body),
                Engine.Layer.Alice => FromAlice(body),
                Engine.Layer.Alexa => FromAlexa(body),
                _ => throw new ArgumentException("This layer type is not supported")
            };
        }

        private Command FromTelegram(string body)
        {
            TelegramUpdate update = JsonConvert.DeserializeObject<TelegramUpdate>(body,Utils.ConverterSettings);
            switch (update.Type)
            {
                case UpdateType.Message:
                    Id = update.Message.MessageId.ToString();
                    Text = update.Message.Text;
                    Payload = "";
                    User = User.FromTelegram(update.Message.From);

                    break;

                case UpdateType.CallbackQuery:
                    Id = update.CallbackQuery.Message.MessageId.ToString();
                    Text = "";
                    Payload = update.CallbackQuery.Data;
                    User = User.FromTelegram(update.CallbackQuery.From);

                    break;

                default: 
                    throw new ArgumentException("This update type is not supported");
                
            }
            return this;
        }

        private Command FromAlice(string body)
        {
            AliceRequest request = JsonConvert.DeserializeObject<AliceRequest>(body, Utils.ConverterSettings);
            Id = request.Session.MessageId.ToString();
            User = User.FromAlice(request.Session);
            Text = request.Request.OriginalUtterance;
            Payload = JsonConvert.SerializeObject(request.Request.Payload,Utils.ConverterSettings);
            AliceCommand = new AliceCommand(request);

            return this;
        }
        private Command FromAlexa(string body)
        {
            return this;
        }

        public bool Check(CommandContent content)
        {
            if (AliceCommand != null)
            {
                return AliceCommand.Check(content);
            }

            if (content.TelegramCommandContent != null)
            {
                return Payload == content.Payload || Text == content.Text;
            }

            throw new ArgumentException("None of layers data were assigned");
        }

    }
}