using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Triglav.Models.Telegram
{
    public class TelegramUpdate
    {
        public int Id { get; set; }
        public Message Message { get; set; }
        public CallbackQuery CallbackQuery { get; set; }
        public UpdateType Type
        {
            get
            {
                if (Message != null) return UpdateType.Message;
                if (CallbackQuery != null) return UpdateType.CallbackQuery;
               
                return UpdateType.Unknown;
            }
        }
    }

    public class Message
    {
        public int MessageId { get; set; }
        public TgUser From { get; set; }
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }
        public Chat Chat { get; set; }
        public bool IsForwarded => ForwardFrom != null;
        public TgUser ForwardFrom { get; set; }
        public Chat ForwardFromChat { get; set; }
        public int ForwardFromMessageId { get; set; }
        public string Text { get; set; }
        public MessageType Type
        {
            get
            {
                if (Text != null)
                    return MessageType.Text;
                
                return MessageType.Unknown;
            }
        }
    }

    public class CallbackQuery
    {
        public string Id { get; set; }
        public TgUser From { get; set; }
        public Message Message { get; set; }
        public string InlineMessageId { get; set; }
        public string ChatInstance { get; set; }
        public string Data { get; set; }
        public string GameShortName { get; set; }
        public bool IsGameQuery => GameShortName != default;
    }
    public class TgUser
    {
        public int Id { get; set; }
        public bool IsBot { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string LanguageCode { get; set; }
        public override string ToString() => (Username == null
                                                 ? FirstName + LastName?.Insert(0, " ")
                                                 : $"@{Username}") +
                                             $" ({Id})";
    }

    public class Chat
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool AllMembersAreAdministrators { get; set; }
        public string Description { get; set; }
        public string InviteLink { get; set; }
        public Message PinnedMessage { get; set; }
        public string StickerSetName { get; set; }
        public bool? CanSetStickerSet { get; set; }
    }
    public enum UpdateType
    {
        Unknown = 0,
        Message,
        InlineQuery,
        ChosenInlineResult,
        CallbackQuery,
        EditedMessage,
        ChannelPost,
        EditedChannelPost,
        ShippingQuery,
        PreCheckoutQuery,
        Poll
    }
    public enum MessageType
    {
        Unknown = 0,
        Text,
        Photo,
        Audio,
        Video,
        Voice,
        Document,
        Sticker,
        Location,
        Contact,
        Venue,
        Game,
        VideoNote,
        Invoice,
        SuccessfulPayment,
        WebsiteConnected,
        ChatMembersAdded,
        ChatMemberLeft,
        ChatTitleChanged,
        ChatPhotoChanged,
        MessagePinned,
        ChatPhotoDeleted,
        GroupCreated,
        SupergroupCreated,
        ChannelCreated,
        MigratedToSupergroup,
        MigratedFromGroup,
        Animation,
        Poll,
    }
}

