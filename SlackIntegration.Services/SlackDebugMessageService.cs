using SlackIntegration.Data;

namespace SlackIntegration.Services
{
    public class SlackDebugMessageService
    {
        public void AddMessage(string message)
        {
            using (var ctx = Context.Create(Configuration.DatabaseConnectionString, true))
            {
                ctx.SlackDebugMessages.Add(new SlackDebugMessages
                {
                    Message = message
                });
                ctx.SaveChanges();
            }
        }
    }
}