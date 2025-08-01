namespace Postal
{
    public class SendResult
    {
        public Dictionary<string, MessageResult> Recipients { get; set; }
    }

    public class MessageResult
    {
        public long Id { get; set; }
        public string Token { get; set; }
    }
}
