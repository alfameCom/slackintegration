using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SlackIntegration.DTO.Slack;
using SlackIntegration.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlackIntegration.Functions.Functions
{
    public class CreateSuccess : FunctionBase
    {
        [FunctionName("CreateSuccess_V1")]
        public static async Task<string> RunV1(
            [ActivityTrigger]
            Payload payload,
            ILogger log)

        {
            log.LogInformation($"CreateSuccess_V1: {DateTime.Now}");

            var receivers = new List<string>();
            var sb = new StringBuilder();

            var praiser = GetUserInfo(payload.user.id).user.real_name;

            sb.Append($"Success <@{payload.submission.usernames1}>");
            receivers.Add(GetUserInfo(payload.submission.usernames1).user.real_name);

            if (payload.submission.usernames2 != null)
            {
                receivers.Add(GetUserInfo(payload.submission.usernames2).user.real_name);
                sb.Append($" <@{payload.submission.usernames2}>");
            }

            if (payload.submission.usernames3 != null)
            {
                receivers.Add(GetUserInfo(payload.submission.usernames3).user.real_name);
                sb.Append($" <@{payload.submission.usernames3}>");
            }

            if (payload.submission.usernames4 != null)
            {
                receivers.Add(GetUserInfo(payload.submission.usernames4).user.real_name);
                sb.Append($" <@{payload.submission.usernames4}>");
            }

            if (payload.submission.usernames5 != null)
            {
                receivers.Add(GetUserInfo(payload.submission.usernames5).user.real_name);
                sb.Append($" <@{payload.submission.usernames5}>");
            }

            sb.Append($" {payload.submission.message}");

            var message = new JObject
            {
                {"text", sb.ToString() },
                {"channel", payload.channel.id},
                {"username", praiser }
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.SlackAccessToken);
            var response = await client.PostAsync("https://slack.com/api/chat.postMessage", new StringContent(message.ToString(), Encoding.UTF8, "application/json"));

            var responseContent = await response.Content.ReadAsStringAsync();

            var successService = new SlackSuccessService();
            successService.Add(praiser, payload.submission.message, receivers);

            return HttpUtility.UrlDecode(responseContent);
        }
    }
}