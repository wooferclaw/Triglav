using System.Collections.Generic;
using System.Linq;
using Triglav.Entities;
using Triglav.Models;

namespace Triglav.Layers
{
    public class AliceCommand
    {
        public string Command { get; set; }
        public List<string> Tokens { get; set; }
        public AliceSession Session { get; set; }
        public string Version { get; set; }

        public AliceCommand(AliceRequest request)
        {
            Command = request.Request.Command;
            Tokens = request.Request.Nlu.Tokens;
            Session = request.Session;
            Version = request.Version;
        }

        public bool Check(CommandContent content)
        {
            return Utils.CheckTokens(Tokens, content.AliceCommandContent.Keywords.ToArray());
        }
    }
}