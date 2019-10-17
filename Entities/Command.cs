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
        

        public Command(Engine.Layer layer, string body)
        {
            switch (layer)
            {
                case Engine.Layer.Alice:
                    FromAlice(body);
                    break;
                case Engine.Layer.Alexa:
                    FromAlexa(body);
                    break;
                case Engine.Layer.Telegram:
                    FromTelegram(body);
                    break;
                case Engine.Layer.VK:
                    throw new NotImplementedException();
                    break;
                case Engine.Layer.Facebook:
                    throw new NotImplementedException();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }

        }
        //make from static or ctor
        private void FromTelegram(string body)
        {
            
            TelegramUpdate update = JsonConvert.DeserializeObject<TelegramUpdate>(body,Utils.ConverterSettings);

            switch (update.Type)
            {
                case UpdateType.Message:
                    Id = update.Message.MessageId.ToString();
                    Text = update.Message.Text;
                    Payload = "";
                    User = new User(update.Message.From);
                    TelegramCommand = new TelegramCommand(update.Message);

                    break;

                case UpdateType.CallbackQuery:
                    Id = update.CallbackQuery.Message.MessageId.ToString();
                    Text = "";
                    Payload = update.CallbackQuery.Data;
                    User = new User(update.CallbackQuery.From);
                    TelegramCommand = new TelegramCommand(update.Message);

                    break;

                default: 
                    throw new ArgumentException("This update type is not supported");
                
            }
            
        }

        private void FromAlice(string body)
        {
            AliceRequest request = JsonConvert.DeserializeObject<AliceRequest>(body, Utils.ConverterSettings);
            Id = request.Session.MessageId.ToString();
            User = new User(request.Session);
            Text = request.Request.OriginalUtterance;
            Payload = JsonConvert.SerializeObject(request.Request.Payload,Utils.ConverterSettings);
            AliceCommand = new AliceCommand(request);
        }
        private Command FromAlexa(string body)
        {
            return this;
        }

        public bool Check(CommandContent content)
        {
            if (AliceCommand != null)
            {
                if (content.IsEnter) return string.IsNullOrEmpty(AliceCommand.Command);
                return AliceCommand.Check(content);
            }

            if (TelegramCommand != null)
            {
                if (content.IsEnter) return Text.StartsWith("/start");
                return Payload == content.Payload || Text == content.Text;
            }

            throw new ArgumentException("None of layers data were assigned");
        }

    }
}