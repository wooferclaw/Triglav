using System.Collections.Generic;
using System.Linq;
using Triglav.Entities;
using Triglav.Models.Alexa;

namespace Triglav.Layers.Alexa
{
    public class AlexaCommand
    {
        public string Version { get; set; }
        public string IntentName { get; set; }
        public string ConfirmationStatus { get; set; }
        public Dictionary<string, string> Slots { get; set; }
        public bool IsLaunchIntent { get; set; }
        public bool IsSessionEnded { get; set; }

        public AlexaCommand(AlexaRequest request)
        {
            IntentName = request.Request.Intent.Name;
            ConfirmationStatus = request.Request.Intent.ConfirmationStatus;
            Slots = request.Request.Intent.Slots;
            IsLaunchIntent = request.Request.Type == "LaunchRequest";
            IsSessionEnded = request.Request.Type == "SessionEndedRequest";
        }
        public bool Check(CommandContent content)
        {
            if (content.AlexaCommandContent.IntentName != null)
                return IntentName == content.AlexaCommandContent.IntentName;

            if (Slots == null || content.AlexaCommandContent.Slots == null) return false;

            return content.AlexaCommandContent.Slots.Keys.All(key => Slots.ContainsKey(key));
        }
    }
}