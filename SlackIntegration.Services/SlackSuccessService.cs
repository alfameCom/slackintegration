using SlackIntegration.Data;
using System.Collections.Generic;

namespace SlackIntegration.Services
{
    public class SlackSuccessService
    {
        /// <summary>
        /// Tallenna successs
        /// </summary>
        /// <param name="praiser">Kuka antoi</param>
        /// <param name="message">Viesti</param>
        /// <param name="receivers">Vastaanottajat</param>
        public void Add(string praiser, string message, List<string> receivers)
        {
            using (var ctx = Context.Create(Configuration.DatabaseConnectionString, true))
            using (var transaction = ctx.Database.BeginTransaction())
            {
                var success = ctx.SlackSuccess.Add(new SlackSuccess
                {
                    Praiser = praiser,
                    Message = message
                });
                ctx.SaveChanges();

                foreach (var receiver in receivers)
                {
                    success.Entity.SlackSuccessReceiver.Add(new SlackSuccessReceiver
                    {
                        Receiver = receiver,
                    });
                }
                ctx.SaveChanges();

                transaction.Commit();
            }
        }
    }
}