using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackIntegration.DTO.Slack;
using SlackIntegration.Services;
using SlackIntegration.Services.Handlers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

            // TODO this is fucking stupid to run here, should be run only once on application startup
            Redirect.Run();

            // Parsitaan sisältö
            var content = req.Content.ReadAsStringAsync().Result;
            content = HttpUtility.UrlDecode(content);

            // Jos sisältö alkaa 'payload=' se sisältää käyttäjän valintoja muuten palautetaan valikko
            if (content == null || !content.StartsWith("payload="))
            {
                var configurationService = new SlackConfigurationService();
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        JsonConvert.SerializeObject(JObject.Parse(configurationService.GetConfiguration("valikko"))),
                        Encoding.UTF8,
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

            log.Info(content);
            try
            {
                HandlePayload(payload, log);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            return req.CreateResponse(HttpStatusCode.OK);
        }

        private static void HandlePayload(Payload payload, TraceWriter log)
        {
            var handler = new HandleSuccess(payload);

            var callbackId = payload.callback_id;

            switch (callbackId)
            {
                case "submit-success":
                    Task.Run(() =>
                    {
                        try
                        {
                            handler.HandleSuccessSubmission();
                        }
                        catch (Exception ex)
                        {
                            log.Error(ex.Message);
                        }
                    });
                    break;

                case "wopr_command":
                    Task.Run(() =>
                    {
                        switch (payload.actions.Single().value)
                        {
                            case "success":
                                handler.Handle();
                                break;
                        }
                    });
                    break;

                default:
                    throw new Exception($"Unknown callbackId: {callbackId}");
            }
        }
    }
}