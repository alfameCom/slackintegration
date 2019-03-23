namespace SlackIntegration.Services
{
    public static class Configuration
    {
        public static string SlackAccessToken
            => System.Environment.GetEnvironmentVariable("SlackAccessToken");

        public static string SlackVerificationToken
            => System.Environment.GetEnvironmentVariable("SlackVerificationToken");

        public static string DatabaseConnectionString
            => System.Environment.GetEnvironmentVariable("DatabaseConnectionString");

        public static string SlackBotUserAccessToken =>
            System.Environment.GetEnvironmentVariable("SlackBotUserAccessToken");
    }
}