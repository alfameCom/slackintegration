using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackIntegration.DTO.Slack;
using SlackIntegration.Services;
using SlackIntegration.Services.Handlers;

namespace SlackIntegration.Functions
{
    /// <summary>
    /// Hämeenlinnan hackaton 2018 koodeista yritetty vääntää sama funktioilla
    /// </summary>
    public static class ReportingBot
    {
        [FunctionName("ReportingBotPost")]
        public static async Task<HttpResponseMessage> Post(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "reportingbot")]
            HttpRequestMessage req,
            TraceWriter log)
        {
            log.Info("ReportingBotPost");

            // Parsitaan sisältö
            var content = req.Content.ReadAsStringAsync().Result;
            content = HttpUtility.UrlDecode(content);

            // Jos sisältö alkaa 'payload=' se sisältää käyttäjän valintoja muuten palautetaan valikko
            if (content == null || !content.StartsWith("payload="))
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(JObject.Parse(DbMock.GetValikko)), Encoding.UTF8,
                        "application/json")
                };
            }

            // Parsitaan payload
            content = content.Substring(8, content.Length - 8);

            Payload payload = null;
            try
            {
                // We hope there is a spoon
                payload = JsonConvert.DeserializeObject<Payload>(content);
            }
            catch (Exception e)
            {
                log.Error("Poikkeus payload parsinnassa", e);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            HandlePayload(payload);
            return req.CreateResponse(HttpStatusCode.OK);
        }

        private static void HandlePayload(Payload payload)
        {
            var action = payload.actions.Single();
            switch (action.value)
            {
                case "success":
                    Task.Run(() =>
                    {
                        var handler = new HandleSuccess(payload);
                        handler.Handle();
                    });
                    break;

                default:
                    throw new Exception($"We have no spoon for {action.value}");
            }
        }
    }
}