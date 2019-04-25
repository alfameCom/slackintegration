using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SlackIntegration.DTO.Slack;
using SlackIntegration.Services;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SlackIntegration.Functions
{
    public class OpenDialog : FunctionBase
    {
        [FunctionName("OpenDialog_V1")]
        public static async Task<string> RunV1(
            [ActivityTrigger]
            Payload payload,
            ILogger log)
        {
            log.LogInformation($"OpenDialog_V1: {DateTime.Now}");

            var configurationService = new SlackConfigurationService();

            var message = new JObject
            {
                { "trigger_id", payload.trigger_id }, { "dialog", JObject.Parse(configurationService.GetConfiguration("success_dialog")) }
            };

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Configuration.SlackAccessToken);
            var response = await client.PostAsync("https://slack.com/api/dialog.open", new StringContent(message.ToString(), Encoding.UTF8, "application/json"));

            var responseContent = await response.Content.ReadAsStringAsync();
            return HttpUtility.UrlDecode(responseContent);
        }
    }
}