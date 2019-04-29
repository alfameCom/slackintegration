using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackIntegration.Services;
using System;

namespace SlackIntegration.Functions
{
    public class GetMenu : FunctionBase
    {
        [FunctionName("GetMenu_V1")]
        public static string RunV1(
            [ActivityTrigger]DurableActivityContext durableContext,
            ILogger log)
        {
            log.LogInformation($"GetMenu_V1: {DateTime.Now}");
            var configurationService = new SlackConfigurationService();
            return JsonConvert.SerializeObject(JObject.Parse(configurationService.GetConfiguration("valikko")));
        }
    }
}