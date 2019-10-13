using System.Collections.Generic;

namespace Triglav.Layers
{
    public class AliceCommandContent
    {
        //public string Keywords { get; set; }
        //public string OriginalUtterance { get; set; }
        //public string Type { get; set; }
        //public string Markup { get; set; }
        //public string NLU { get; set; }

        //Свой Check

        public class Interfaces
        {
            public object Screen { get; set; }
        }

        public class Meta
        {
            public string Locale { get; set; }
            public string Timezone { get; set; }
            public string ClientId { get; set; }
            public Interfaces Interfaces { get; set; }
        }

        public class Markup
        {
            public bool DangerousContext { get; set; }
        }

        public class Payload
        {
        }

        public class Tokens
        {
            public int Start { get; set; }
            public int End { get; set; }
        }

        public class Entity
        {
            public Tokens Tokens { get; set; }
            public string Type { get; set; }
            public object Value { get; set; }
        }

        public class Nlu
        {
            public List<string> Tokens { get; set; }
            public List<Entity> Entities { get; set; }
        }

        public class Request
        {
            //public string Command { get; set; }
            public string Original_utterance { get; set; }
            public string Type { get; set; }
            public Markup Markup { get; set; }
            //public Payload Payload { get; set; }
            public Nlu Nlu { get; set; }
        }

        public class Session
        {
            public bool @New { get; set; }
            public int MessageId { get; set; }
            public string SessionId { get; set; }
            public string SkillId { get; set; }
            public string UserId { get; set; }
        }

        public class RootObject
        {
            public Meta meta { get; set; }
            public Request request { get; set; }
            public Session session { get; set; }
            public string version { get; set; }
        }

    }
}