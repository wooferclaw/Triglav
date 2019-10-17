using System.Collections.Generic;
using System.Linq;

namespace Triglav.Models
{
    public class TelegramSendMessage
    {
        public string Method { get; set; } = "sendMessage";
        public int ChatId { get; set; }
        public string Text { get; set; }
        public string ParseMode { get; set; }
        public bool DisableWebPagePreview { get; set; }
        public bool DisableNotifications { get; set; }
        public int ReplyToMessageId { get; set; }
        public ReplyMarkupBase ReplyMarkup { get; set; }
    }

    public abstract class ReplyMarkupBase
    {
        public bool Selective { get; set; }
    }

    public class ReplyKeyboardMarkup : ReplyMarkupBase
    {
        public IEnumerable<IEnumerable<KeyboardButton>> Keyboard { get; set; }

        public bool ResizeKeyboard { get; set; } = true;
        public bool OneTimeKeyboard { get; set; }

        public ReplyKeyboardMarkup()
        {
        }

        public ReplyKeyboardMarkup(KeyboardButton button)
            : this(new[] {button})
        {
        }

        public ReplyKeyboardMarkup(IEnumerable<KeyboardButton> keyboardRow, bool resizeKeyboard = default,
            bool oneTimeKeyboard = default)
            : this(new[] {keyboardRow}, resizeKeyboard, oneTimeKeyboard)
        {
        }

        public ReplyKeyboardMarkup(IEnumerable<IEnumerable<KeyboardButton>> keyboard, bool resizeKeyboard = default,
            bool oneTimeKeyboard = default)
        {
            Keyboard = keyboard;
            ResizeKeyboard = resizeKeyboard;
            OneTimeKeyboard = oneTimeKeyboard;
        }

        public static implicit operator ReplyKeyboardMarkup(string text) =>
            text == null
                ? default
                : new ReplyKeyboardMarkup(new[] {new KeyboardButton(text)});

        public static implicit operator ReplyKeyboardMarkup(string[] texts) =>
            texts == null
                ? default
                : new[] {texts};

        public static implicit operator ReplyKeyboardMarkup(string[][] textsItems) =>
            textsItems == null
                ? default
                : new ReplyKeyboardMarkup(
                    textsItems.Select(texts =>
                        texts.Select(t => new KeyboardButton(t))
                    ));
    }

    public class InlineKeyboardMarkup : ReplyMarkupBase
    {
        public IEnumerable<IEnumerable<InlineKeyboardButton>> InlineKeyboard { get; }

        public InlineKeyboardMarkup(InlineKeyboardButton inlineKeyboardButton)
            : this(new[] {inlineKeyboardButton})
        {
        }

        public InlineKeyboardMarkup(IEnumerable<InlineKeyboardButton> inlineKeyboardRow)
        {
            InlineKeyboard = new[]
            {
                inlineKeyboardRow
            };
        }

        public InlineKeyboardMarkup(IEnumerable<IEnumerable<InlineKeyboardButton>> inlineKeyboard)
        {
            InlineKeyboard = inlineKeyboard;
        }

        public static InlineKeyboardMarkup Empty() =>
            new InlineKeyboardMarkup(new InlineKeyboardButton[0][]);

        public static implicit operator InlineKeyboardMarkup(InlineKeyboardButton button) =>
            button == null
                ? default
                : new InlineKeyboardMarkup(button);

        public static implicit operator InlineKeyboardMarkup(string buttonText) =>
            buttonText == null
                ? default
                : new InlineKeyboardMarkup(buttonText);

        public static implicit operator InlineKeyboardMarkup(IEnumerable<InlineKeyboardButton>[] inlineKeyboard) =>
            inlineKeyboard == null
                ? null
                : new InlineKeyboardMarkup(inlineKeyboard);

        public static implicit operator InlineKeyboardMarkup(InlineKeyboardButton[] inlineKeyboard) =>
            inlineKeyboard == null
                ? null
                : new InlineKeyboardMarkup(inlineKeyboard);
    }

    public class InlineKeyboardButton
    {
        public string Text { get; set; }
        public string Url { get; set; }
        public string CallbackData { get; set; }
        public string SwitchInlineQuery { get; set; }
        public string SwitchInlineQueryCurrentChat { get; set; }
        public bool Pay { get; set; }

        public static InlineKeyboardButton WithUrl(string text, string url) =>
            new InlineKeyboardButton
            {
                Text = text,
                Url = url
            };

        public static InlineKeyboardButton WithCallbackData(string textAndCallbackData) =>
            new InlineKeyboardButton
            {
                Text = textAndCallbackData,
                CallbackData = textAndCallbackData
            };

        public static InlineKeyboardButton WithCallbackData(string text, string callbackData) =>
            new InlineKeyboardButton
            {
                Text = text,
                CallbackData = callbackData
            };

        public static InlineKeyboardButton WithSwitchInlineQuery(string text, string query = "") =>
            new InlineKeyboardButton
            {
                Text = text,
                SwitchInlineQuery = query
            };

        public static InlineKeyboardButton WithSwitchInlineQueryCurrentChat(string text, string query = "") =>
            new InlineKeyboardButton
            {
                Text = text,
                SwitchInlineQueryCurrentChat = query
            };

        public static InlineKeyboardButton WithPayment(string text) =>
            new InlineKeyboardButton
            {
                Text = text,
                Pay = true
            };

        public static implicit operator InlineKeyboardButton(string textAndCallbackData) =>
            textAndCallbackData == null
                ? default
                : WithCallbackData(textAndCallbackData);
    }

    public class KeyboardButton
    {
        public string Text { get; set; }
        public bool RequestLocation { get; set; }
        public bool RequestContact { get; set; }

        public KeyboardButton()
        {
        }

        public KeyboardButton(string text)
        {
            Text = text;
        }

        public static KeyboardButton WithRequestContact(string text) =>
            new KeyboardButton(text) {RequestContact = true};

        public static KeyboardButton WithRequestLocation(string text) =>
            new KeyboardButton(text) {RequestLocation = true};

        public static implicit operator KeyboardButton(string text)
            => new KeyboardButton(text);
    }
}