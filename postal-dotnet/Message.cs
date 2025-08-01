
namespace Postal
{
    public class Message
    {
        public List<string> To { get; set; } = new List<string>();
        public List<string> Cc { get; set; } = new List<string>();
        public List<string> Bcc { get; set; } = new List<string>();
        public string From { get; set; }
        public string Subject { get; set; }
        public string PlainBody { get; set; }
        public string HtmlBody { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }

    public class Attachment
    {
        public string Filename { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
