using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SlackIntegration.DTO.Slack;
using SlackIntegration.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SlackIntegration.Functions
{
    public class ReportingBot : FunctionBase
    {
        [FunctionName("ReportingBotPost_V1")]
        public static async Task<HttpResponseMessage> PostV1(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "reportingbot")]
            HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient orchestrationClient,
            ILogger log)
        {
            var content = await req.Content.ReadAsStringAsync();
            var instanceId = await orchestrationClient.StartNewAsync("ReportingBotRunOrchestrator_V1", content);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            // Orkestroinnin doc: https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-instance-management
            // Retry tuntuu määrittävän milloin vastausta "pollataan", joten on pakko olla lyhyt tässä tapauksessa
            // Slack odottaa vastausta < 3 sec ?
            var result = await orchestrationClient.WaitForCompletionOrCreateCheckStatusResponseAsync(
                req,
                instanceId,
                TimeSpan.FromSeconds(30),
                TimeSpan.FromMilliseconds(100));

            // Orkestroinnin vastaus on aina HttpResponseMessage jonka sisällä orkestrointi
            // funktion vastaus json muodossa
            var resultContent = await result.Content.ReadAsStringAsync();
            var myHttpResponse = JsonConvert.DeserializeObject<SimpleHttpResponse>(resultContent);
            return CreateHttpResponseMessage(myHttpResponse);
        }

        [FunctionName("ReportingBotRunOrchestrator_V1")]
        public static async Task<SimpleHttpResponse> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context,
            ILogger log)
        {
            // TODO
            // Seuraava kehityskohde olisi palautta vastaus mahdollisimman pian
            // Sen jälken vain "fire and forget" noi jotka lähettää takaisin Slack:in
            // päin viestin.

            // Poimitaan sisältö
            var content = context.GetInput<string>();
            content = HttpUtility.UrlDecode(content);
            await context.CallActivityAsync("SaveContent_V1", content);

            // Jos sisältö alkaa 'payload=' se sisältää käyttäjän valintoja muuten palautetaan valikko
            if (content == null || !content.StartsWith("payload="))
            {
                var menu = await context.CallActivityAsync<string>("GetMenu_V1", null);
                return new SimpleHttpResponse(HttpStatusCode.OK)
                {
                    Content = menu,
                };
            }

            // Poimitaan vain  json objekti
            content = content.Substring(content.IndexOf("{"));

            // Parsitaan
            Payload payload = await context.CallActivityAsync<Payload>("ParsePayload_V1", content);
            if (payload == null)
            {
                log.LogError($"payload null");
                return new SimpleHttpResponse(HttpStatusCode.InternalServerError);
            }

            // Tarkistetaan
            if (payload.token != Configuration.SlackVerificationToken)
            {
                log.LogError($"payload token does not match, token:{payload.token}, user:{payload.user?.real_name}");
                return new SimpleHttpResponse(HttpStatusCode.InternalServerError);
            }

            // Seuraava toimenpide?
            switch (payload.callback_id)
            {
                case "wopr_command":
                    {
                        var response = await context.CallActivityAsync<string>("OpenDialog_V1", payload);
                        await context.CallActivityAsync("SaveContent_V1", response);
                        return new SimpleHttpResponse(HttpStatusCode.OK);
                    }

                case "submit-success":
                    {
                        var response = await context.CallActivityAsync<string>("CreateSuccess_V1", payload);
                        await context.CallActivityAsync("SaveContent_V1", response);
                        return new SimpleHttpResponse(HttpStatusCode.OK);
                    }

                default:
                    {
                        log.LogError($"Unknown callbackId: {payload.callback_id}");
                        return new SimpleHttpResponse(HttpStatusCode.InternalServerError);
                    }
            }
        }
    }
}