using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using Triglav.Layers.Alexa;
using Triglav.Layers.Alice;
using Triglav.Layers.Telegram;
using Triglav.Models.Alexa;

namespace Triglav.Entities
{
    public class MessageContent
    {
        public Dictionary<Locale, string> Text { get; set; }
        public Dictionary<Locale, string[]> Buttons { get; set; }
        public bool InlineButtons { get; set; }

        public AliceMessageContent AliceMessageContent { get; set; }
        public AlexaMessageContent AlexaMessageContent { get; set; }
        public TelegramMessageContent TelegramMessageContent { get; set; }

        public OutputSpeech AsOutputSpeech(Locale locale)
        {
            if (AlexaMessageContent?.Ssml != null)
            {
                return new OutputSpeech
                {
                    SSML = $"<speak>{AlexaMessageContent.Ssml}</speak>",
                    Type = "SSML",
                    PlayBehavior = "REPLACE_ALL"
                };
            }

            return new OutputSpeech
            {
                Text = Text[locale],
                Type = "PlainText",
                PlayBehavior = "REPLACE_ALL"
            };
        }
    }
}