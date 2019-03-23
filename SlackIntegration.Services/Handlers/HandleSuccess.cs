using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackIntegration.DTO.Slack;

namespace SlackIntegration.Services.Handlers
{
    /// <summary>
    ///
    /// </summary>
    public class HandleSuccess
    {
        private SlackConfigurationService _configurationService;
        private readonly Payload _payload;

        /// <summary>
        ///
        /// </summary>
        /// <param name="payload"></param>
        public HandleSuccess(Payload payload)
        {
            _payload = payload;
            _configurationService = new SlackConfigurationService();
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
                { "trigger_id", _payload.trigger_id }, { "dialog", JObject.Parse(_configurationService.GetConfiguration("success_dialog")) }
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.SlackAccessToken);
            var response = client.PostAsync("https://slack.com/api/dialog.open", new StringContent(message.ToString(), Encoding.UTF8, "application/json")).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;
            responseContent = HttpUtility.UrlDecode(responseContent);
        }

        public void HandleSuccessSubmission()
        {
            var receivers = new List<string>();
            var sb = new StringBuilder();

            var praiser = GetUserInfo(_payload.user.id).user.real_name;

            sb.Append($"Success <@{_payload.submission.usernames1}>");
            receivers.Add(GetUserInfo(_payload.submission.usernames1).user.real_name);

            if (_payload.submission.usernames2 != null)
            {
                receivers.Add(GetUserInfo(_payload.submission.usernames2).user.real_name);
                sb.Append($" <@{_payload.submission.usernames2}>");
            }

            if (_payload.submission.usernames3 != null)
            {
                receivers.Add(GetUserInfo(_payload.submission.usernames3).user.real_name);
                sb.Append($" <@{_payload.submission.usernames3}>");
            }

            if (_payload.submission.usernames4 != null)
            {
                receivers.Add(GetUserInfo(_payload.submission.usernames4).user.real_name);
                sb.Append($" <@{_payload.submission.usernames4}>");
            }

            if (_payload.submission.usernames5 != null)
            {
                receivers.Add(GetUserInfo(_payload.submission.usernames5).user.real_name);
                sb.Append($" <@{_payload.submission.usernames5}>");
            }

            sb.Append($" {_payload.submission.message}");

            var message = new JObject
            {
                {"text", sb.ToString() },
                {"channel", _payload.channel.id},
                {"username", praiser }
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.SlackAccessToken);
            var response = client.PostAsync("https://slack.com/api/chat.postMessage", new StringContent(message.ToString(), Encoding.UTF8, "application/json")).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;
            responseContent = HttpUtility.UrlDecode(responseContent);

            var successService = new SlackSuccessService();
            successService.Add(praiser, _payload.submission.message, receivers);
        }

        public UserResponse GetUserInfo(string userId)
        {
            using (var httpClient = new HttpClient())
            {
                var accesToken = Configuration.SlackBotUserAccessToken;

                var url = $"https://slack.com/api/users.info?token={accesToken}&user={userId}&pretty=1";

                var response = httpClient.GetAsync(url).Result;

                var responseContent = response.Content.ReadAsStringAsync().Result;
                responseContent = HttpUtility.UrlDecode(responseContent);
                return JsonConvert.DeserializeObject<UserResponse>(responseContent);
            }
        }
    }
}