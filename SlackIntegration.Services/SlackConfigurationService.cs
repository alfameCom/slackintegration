using SlackIntegration.Data;
using System;
using System.Linq;

namespace SlackIntegration.Services
{
    public class SlackConfigurationService
    {
        public string GetConfiguration(string key)
        {
            using (var ctx = Context.Create(Configuration.DatabaseConnectionString, false))
            {
                var configuration = ctx.SlackConfiguration.FirstOrDefault(c => c.Key == key);
                if (configuration == null)
                {
                    throw new Exception($"Can't find configuraion with key:{key}");
                }

                return configuration.Value;
            }
        }
    }
}