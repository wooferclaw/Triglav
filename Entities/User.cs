using System;
using Triglav.Models.Alice;
using Triglav.Models.Telegram;

namespace Triglav.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }

        public User(AliceSession session)
        {
            Id = session.UserId;
            Name = "";
            Domain = "";
        }

        public User(TgUser user)
        {
            Id = user.Id.ToString();
            Name = user.FirstName + " " + user.LastName;
            Domain = user.Username;
        }

        public User(Models.Alexa.User user)
        {
            Id = user.UserId;
            Name = "";
            Domain = "";
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