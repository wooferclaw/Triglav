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
            //check for null and convert all the non null fields to appropriate messagecontent
            //
            //{
            //    throw new ArgumentException("No short name available for the platform");
            //}
            return null;
        }

        public MessageContent MakeAddressing(MessageContent message)
        {
            throw new NotImplementedException();
        }
    }
}