using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;
using SlackIntegration.DTO.Slack;

namespace SlackIntegration.Services.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public class HandleSuccess
    {
        private readonly Payload _payload;

        /// <summary>
        ///
        /// </summary>
        /// <param name="payload"></param>
        public HandleSuccess(Payload payload)
        {
            _payload = payload;
        }

        /// <summary>
        ///
        /// </summary>
        public void Handle()
        {
            // check it is valid
            if (_payload.token != Configuration.SlackVerificationToken)
            {
                throw new Exception("This sure must help");
            }

            var message = new JObject
            {
                { "trigger_id", _payload.trigger_id }, { "dialog", JObject.Parse(DbMock.GetSuccessDialog) }
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.SlackAccessToken);
            var response = client.PostAsync("https://slack.com/api/dialog.open", new StringContent(message.ToString(), Encoding.UTF8, "application/json")).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;
            responseContent = HttpUtility.UrlDecode(responseContent);
        }

        public void HandleSuccessSubmission()
        {
            throw new NotImplementedException();
        }
    }
}