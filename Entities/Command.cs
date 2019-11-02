using System;
using System.Linq;
using Newtonsoft.Json;
using Triglav.Layers.Alexa;
using Triglav.Layers.Alice;
using Triglav.Layers.Telegram;
using Triglav.Models.Alexa;
using Triglav.Models.Alice;
using Triglav.Models.Telegram;

namespace Triglav.Entities
{
    public class Command
    {
        public string Id { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string Payload { get; set; }

        public AliceCommand AliceCommand { get; set; }
        public AlexaCommand AlexaCommand { get; set; }
        public TelegramCommand TelegramCommand { get; set; }
        
        public Command(Layer layer, string body)
        {
            switch (layer)
            {
                case Layer.Alice:
                    FromAlice(body);
                    break;
                case Layer.Alexa:
                    FromAlexa(body);
                    break;
                case Layer.Telegram:
                    FromTelegram(body);
                    break;
                case Layer.VK:
                    throw new NotImplementedException();
                case Layer.Facebook:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(layer), layer, null);
            }

        }
        //make from static or ctor
        private void FromTelegram(string body)
        {
            TelegramUpdate update = JsonConvert.DeserializeObject<TelegramUpdate>(body, Utils.ConverterSettingsSnake);

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
            AliceRequest request = JsonConvert.DeserializeObject<AliceRequest>(body, Utils.ConverterSettingsSnake);
            Id = request.Session.MessageId.ToString();
            User = new User(request.Session);
            Text = request.Request.OriginalUtterance;
            Payload = JsonConvert.SerializeObject(request.Request.Payload,Utils.ConverterSettingsSnake);
            AliceCommand = new AliceCommand(request);
        }
        
        private void FromAlexa(string body)
        {
            var simpleRequest = JsonConvert.DeserializeObject<SimpleAlexaRequest>(body,Utils.ConverterSettingsCamel);

            switch (simpleRequest.Request.Type)
            {
                case "LaunchRequest":
                {
                    var request = JsonConvert.DeserializeObject<AlexaLaunchRequest>(body, Utils.ConverterSettingsCamel);

                    Id = request.Request.RequestId;
                    User = new User(request.Session.User);
                    Text = "";
                    Payload = "";
                    AlexaCommand = new AlexaCommand(request);
                    break;
                }
                case "IntentRequest":
                {
                    var request = JsonConvert.DeserializeObject<AlexaIntentRequest>(body, Utils.ConverterSettingsCamel);

                    Id = request.Request.RequestId;
                    User = new User(request.Session.User);
                    Text = request.Request?.Intent?.Slots != null && request.Request.Intent.Slots.Keys.Count > 0
                        ? request.Request.Intent.Slots.First().Value.Value
                        : "";
                    Payload = "";
                    AlexaCommand = new AlexaCommand(request);
                    break;
                }
                case "SessionEndedRequest":
                {
                    var request = JsonConvert.DeserializeObject<AlexaSessionEndedRequest>(body, Utils.ConverterSettingsCamel);

                    Id = request.Request.RequestId;
                    User = new User(request.Session.User);
                    Text = "";
                    Payload = "";
                    AlexaCommand = new AlexaCommand(request);
                    break;
                }
            }
        }

        public bool Check(CommandContent content, Locale locale)
        {
            if (content.Text != null && !content.Text.ContainsKey(locale))
            {
                throw new ArgumentException("Locale is not supported");
            }
            
            if (AliceCommand != null)
            {
                Utils.CheckLocale(Layer.Alice, locale);
                if (content.IsEnter) return string.IsNullOrEmpty(AliceCommand.Command);
                return AliceCommand.Check(content);
            }

            if (TelegramCommand != null)
            {
                Utils.CheckLocale(Layer.Telegram, locale);
                if (content.IsEnter) return Text.StartsWith("/start");
                if (content.Text == null) return Payload == content.Payload;
                return content.Text[locale] == Text;
            }

            if (AlexaCommand != null)
            {
                Utils.CheckLocale(Layer.Alexa, locale);
                if (content.IsEnter) return AlexaCommand.IsLaunchIntent;
                return AlexaCommand.Check(content);
            }

            throw new ArgumentException("None of layers data were assigned");
        }

    }
}