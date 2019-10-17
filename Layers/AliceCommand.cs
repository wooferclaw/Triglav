using System.Collections.Generic;

namespace Triglav.Layers
{
    public class AliceCommand
    {
        public string Command { get; set; }
        public List<string> Tokens { get; set; }
        public AliceSession Session { get; set; }
    }

    public class AliceSession
    {
        public bool New { get; set; }
        public int MessageId { get; set; }
        public string SessionId { get; set; }
        public string SkillId { get; set; }
        public string UserId { get; set; }
    }
}