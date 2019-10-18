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

        public Message(MessageContent messageContent, User user, Command command = null)
        {
            User = user;
            Command = command;
            Content = messageContent;
        }

        private Command Command { get; set; }

        public string As(Layer layer, Locale locale)
        {
            Utils.CheckLocale(layer, locale);
            
            return layer switch
            {
                Layer.Telegram => AsTelegram(locale),
                Layer.Alice => AsAlice(),
                Layer.Alexa => AsAlexa(),
                _ => throw new ArgumentOutOfRangeException(nameof(layer), "This layer type is not supported")
            };
        }

        private string AsAlice()
        {
            if (Command == null)
            {
                throw new NullReferenceException("Message should respond to incoming command!");
            }

            var response = new AliceResponse
            {
                Response = new Response()
                {
                    Text = Content.Text[Locale.Ru],
                    Buttons = new List<Button>(),
                },
                Session = Command.AliceCommand.Session,
                Version = Command.AliceCommand.Version
            };
            if (Content.AliceMessageContent != null)
            {
                response.Response.Tts = Content.AliceMessageContent.Tts;
                response.Response.EndSession = Content.AliceMessageContent.EndSession;
            }

            if (Content.Buttons != null)
            {
                response.Response.Buttons = Content.Buttons[Locale.Ru].Select(x => new Button
                {
                    Title = x,
                    Hide = !Content.InlineButtons
                }).ToList();
            }

            return JsonConvert.SerializeObject(response, Utils.ConverterSettings);
        }

        private string AsTelegram(Locale locale)
        {
            
            var response = new TelegramSendMessage()
            {
                Method = "sendMessage",
                ChatId = int.Parse(User.Id),
                Text = Content.Text[locale],
            };

            var buttons = new List<List<string>>();
            var telegramMessageContent = Content.TelegramMessageContent ?? new TelegramMessageContent();

            if (!telegramMessageContent.ParseMode.IsNullOrEmpty())
            {
                response.ParseMode = telegramMessageContent.ParseMode;
            }

            //if (Command!= null)
            //    response.ReplyToMessageId = int.Parse(Command.Id);
            if (Content.Buttons != null && Content.Buttons[locale].Length != 0)
            {
                if (telegramMessageContent.ButtonsByRows != null)
                {
                    var c = 0;
                    foreach (var row in telegramMessageContent.ButtonsByRows)
                    {
                        buttons.Add(new List<string>());
                        for (var i = 0; i < row; i++)
                        {
                            buttons.Last().Add(Content.Buttons[locale][c++]);
                        }
                    }
                }
                else
                {
                    buttons.Add(Content.Buttons[locale].ToList());
                }
            }

            // create keyboards
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

                ((ReplyKeyboardMarkup) response.ReplyMarkup).OneTimeKeyboard = telegramMessageContent.OneTimeKeyboard;
            }

            return JsonConvert.SerializeObject(response, Utils.ConverterSettings);
        }
        
        private string AsAlexa()
        {
            throw new NotImplementedException();
        }

    }
}