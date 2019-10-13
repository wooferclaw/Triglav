namespace Triglav
{
    public class Message
    {
        //generic depending on parameter value?
        public Message(MessageContent messageContent, User user)
        {
            
        }
        public string Target { get; set; }

        //convert to aliceresponce or telegram responce (take similar fields and return them as necessary)
        public string As(Engine.Layer layer)
        {
            return "";
        }
    }
}