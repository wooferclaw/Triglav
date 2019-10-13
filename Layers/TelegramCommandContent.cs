using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Triglav.Layers
{
    public class TelegramCommandContent
    {
        public class Message
        {
            public int MessageId { get; set; }
            public User From { get; set; }
            [JsonConverter(typeof(UnixDateTimeConverter))]
            public DateTime Date { get; set; }
            public User ForwardFrom { get; set; }
            public int ForwardFromMessageId { get; set; }
            public string ForwardSignature { get; set; }
            public string ForwardSenderName { get; set; }
            [JsonConverter(typeof(UnixDateTimeConverter))]
            public DateTime? ForwardDate { get; set; }
            public Message ReplyToMessage { get; set; }
            [JsonConverter(typeof(UnixDateTimeConverter))]
            public DateTime? EditDate { get; set; }
            public string MediaGroupId { get; set; }
            public string AuthorSignature { get; set; }
            public string Text { get; set; }
            public MessageEntity[] Entities { get; set; }
            public IEnumerable<string> EntityValues =>
                Entities?.Select(entity => Text.Substring(entity.Offset, entity.Length));
            public PhotoSize[] Photo { get; set; }
            public string ReplyMarkup { get; set; }

            public class PhotoSize
            {
                public string FileId { get; set; }
                public int FileSize { get; set; }
                public int Width { get; set; }
                public int Height { get; set; }
            }

            public class MessageEntity
            {
                public string Type { get; set; }
                public int Offset { get; set; }
                public int Length { get; set; }
                public string Url { get; set; }
                public User User { get; set; }
            }

            public bool CheckCommand(Command incoming, CommandContent expected)
            {
                //Алгоритм проверки входящих данных от пользователя
                throw new NotImplementedException();
            }

        }
    }
}