using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using SlackIntegration.Services;
using System;

namespace SlackIntegration.Functions
{
    public class SaveContent : FunctionBase
    {
        [FunctionName("SaveContent_V1")]
        public static string RunV1(
            [ActivityTrigger]
            string content,
            ILogger log)
        {
            log.LogInformation($"SaveContent_V1: {DateTime.Now}");

            // Tallennetaan kantaan mitä slack meille lähetti
            if (!string.IsNullOrEmpty(content))
            {
                var debugMessageService = new SlackDebugMessageService();
                debugMessageService.AddMessage(content);
            }

            return content;
        }
    }
}