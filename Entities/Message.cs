using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Triglav.Layers;
using Triglav.Models;

namespace Triglav.Entities
{
    public class Message
    {
        public User User { get; set; }
        public MessageContent Content { get; set; }
        public Message(MessageContent messageContent, User user)
        {
            User = user;
            Content = messageContent;
        }

        private Command Command { get; set; }

        public Message(Command command)
        {
            Command = command;
        }

        public string As(Engine.Layer layer)
        {
            return layer switch
            {
                Engine.Layer.Telegram => AsTelegram(),
                Engine.Layer.Alice => AsAlice(),
                Engine.Layer.Alexa => AsAlexa(),
                _ => throw new ArgumentException("This layer type is not supported")
            };
        }

        private string AsAlice()
        {
            if (Command == null) throw new ArgumentException("Message should respond to incoming command!");

            var response = new AliceResponse
            {
                Response = new Response()
                {
                    Text = Content.Text,
                    Tts = Content.AliceMessage.Tts,
                    Buttons = new List<Button>(),
                    EndSession = Content.AliceMessage.EndSession
                },
                Session = Command.AliceCommand.Session,
                Version = Command.AliceCommand.Version
            };

            if (Content.Buttons != null)
            {
                response.Response.Buttons = Content.Buttons.Select(x => new Button
                {
                    Title = x,
                    Hide = !Content.InlineButtons
                }).ToList();
            }

            return JsonConvert.SerializeObject(response, Utils.ConverterSettings);
        }

        private string AsTelegram()
        {
            var response = new TelegramSendMessage()
            {
                Method = "sendMessage",
                ChatId = int.Parse(User.Id),
                Text = Content.Text,
                ParseMode = Content.TelegramMessage.ParseMode,
            };

            if (Command!= null)
                response.ReplyToMessageId = int.Parse(Command.Id);

            var buttons = new List<List<string>>();
            var c = 0;
            foreach (var row in Content.TelegramMessage.ButtonsByRows)
            {
                buttons.Add(new List<string>());
                for (int i = 0; i < row; i++)
                { 
                    buttons.Last().Add(Content.Buttons[c++]);
                }
            }

            if (Content.InlineButtons)
                response.ReplyMarkup = new InlineKeyboardMarkup(buttons.Select(x => x.Select(y =>
                        new InlineKeyboardButton
                        {
                            Text = y,
                            CallbackData = y
                        }
                    )
                ));
            else
            {
                response.ReplyMarkup = new ReplyKeyboardMarkup(buttons.Select(x => x.Select(y =>
                        new KeyboardButton()
                        {
                            Text = y,
                            RequestContact = false,
                            RequestLocation = false
                        }
                    )
                ));
            }    

            return JsonConvert.SerializeObject(response, Utils.ConverterSettings);
        }
        private string AsAlexa()
        {
            throw new NotImplementedException();
        }

    }
}