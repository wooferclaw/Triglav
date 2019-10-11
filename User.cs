using System;
using System.Diagnostics.CodeAnalysis;

namespace Triglav
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

        public MessageContent MakeMention(MessageContent message)
        {
            throw new NotImplementedException();
        }

        public MessageContent MakeAddressing(MessageContent message)
        {
            throw new NotImplementedException();
        }
    }
}