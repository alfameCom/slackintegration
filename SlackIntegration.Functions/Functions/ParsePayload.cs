using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackIntegration.DTO.Slack;
using System;

namespace SlackIntegration.Functions
{
    public class ParsePayload : FunctionBase
    {
        [FunctionName("ParsePayload_V1")]
        public static Payload RunV1(
            [ActivityTrigger]
        string content,
            ILogger log)
        {
            log.LogInformation($"ParsePayload_V1: {DateTime.Now}");

            try
            {
                return JsonConvert.DeserializeObject<Payload>(content);
            }
            catch (Exception e)
            {
                log.LogError("Poikkeus payload parsinnassa", e);
                return null;
            }
        }
    }
}