using System;
using System.Diagnostics.CodeAnalysis;
using Triglav.Entities;
using Triglav.Models;

namespace Triglav
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

        public User FromAlice(AliceSession session)
        {
            Id = session.UserId;
            Name = "";
            Domain = "";

            return this;
        }

        public User FromTelegram(TgUser user)
        {
            Id = user.Id.ToString();
            Name = user.FirstName + " " + user.LastName;
            Domain = user.Username;

            return this;
        }

        public User FromAlexa(AlexaUser user)
        {
            Id = user.Id;
            Name = "";
            Domain = "";

            return this;
        }
        public MessageContent MakeMention(MessageContent message)
        {

            //check for null and convert all the non null fields to appropriate message_content
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