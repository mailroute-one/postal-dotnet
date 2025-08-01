using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MailrouteOne.Postal
{
    public class Client
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public Client(string baseUrl, string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<SendResult> SendMessageAsync(Message message)
        {
            var requestData = new
            {
                to = message.To,
                cc = message.Cc,
                bcc = message.Bcc,
                from = message.From,
                subject = message.Subject,
                plain_body = message.PlainBody,
                html_body = message.HtmlBody,
                attachments = message.Attachments.Select(a => new
                {
                    name = a.Filename,
                    content_type = a.ContentType,
                    data = Convert.ToBase64String(a.Data)
                }),
                headers = message.Headers
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("x-server-api-key", _apiKey);

            var response = await _httpClient.PostAsync("/api/v1/send/message", content);

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<SendResult>(responseString);
        }
    }
}
