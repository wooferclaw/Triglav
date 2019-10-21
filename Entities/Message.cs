using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Triglav.Layers;
using Triglav.Layers.Telegram;
using Triglav.Models.Alexa;
using Triglav.Models.Alice;
using Triglav.Models.Telegram;

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
                Layer.Alexa => AsAlexa(locale),
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
                Response = new Models.Alice.Response()
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

            return JsonConvert.SerializeObject(response, Utils.ConverterSettingsSnake);
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

            return JsonConvert.SerializeObject(response, Utils.ConverterSettingsSnake);
        }

        private string AsAlexa(Locale locale)
        {
            var response = new AlexaResponse
            {
                Response = new Models.Alexa.Response()
            };

            var outputSpeech = Content.AsOutputSpeech(locale);

            if (Content.AlexaMessageContent.Reprompt != null)
            {
                response.Response.Reprompt = new Reprompt {OutputSpeech = outputSpeech};
            }
            response.Response.OutputSpeech = outputSpeech;
            
            response.Version = Command.AlexaCommand.Version;
            response.Response.ShouldEndSession = Content.AlexaMessageContent.ShouldEndSession;

            return JsonConvert.SerializeObject(response, Utils.ConverterSettingsCamel); 
        }

        public string AsAliceTts(Locale locale, SoundBank soundBank = null)
        {
            soundBank ??= SoundBank.DefaultSoundBank;

            var regex = new Regex(Utils.SoundBankPattern);
            var matches = regex.Matches(Content.Text[locale]);

            var result = Content.Text[locale];

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var genTts = match.Value.TrimStart('[').TrimEnd(']').Split('|');

                if (genTts[0] == "audio")
                {
                    result = result.Replace(match.Value, $"<speaker audio=\"{soundBank.Get(Layer.Alice, genTts[1])}\">");
                }
                if (genTts[0] == "pause")
                {
                    //TODO ---CHECK DOCUMENTATION!---
                    result = result.Replace(match.Value, $"<sil[{genTts[1]}]>");
                }
                if (genTts[0] == "accent")
                {
                    result = result.Replace(match.Value, "+");
                }
            }

            return result;
        }

        public string AsAlexaSsml(Locale locale, SoundBank soundBank = null)
        {
            soundBank ??= SoundBank.DefaultSoundBank;

            var regex = new Regex(Utils.SoundBankPattern);
            var matches = regex.Matches(Content.Text[locale]);

            var result = Content.Text[locale];

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                var genTts = match.Value.TrimStart('[').TrimEnd(']').Split('|');

                if (genTts[0] == "audio")
                {
                    result = result.Replace(match.Value, $"<audio src=\"{soundBank.Get(Layer.Alexa,genTts[1])}\">");
                }
                if (genTts[0] == "pause")
                {
                    //TODO ---CHECK DOCUMENTATION!---
                    result = result.Replace(match.Value, $"<break time=\"{genTts[1]}\">");
                }
                if (genTts[0] == "accent")
                {
                    result = result.Replace(match.Value, ""); 
                }
            }

            return result;
        }
        public string AsText(Locale locale)
        {
            var regex = new Regex(Utils.SoundBankPattern);
            return regex.Replace(Content.Text[locale],"");
        }
    }
}