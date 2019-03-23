namespace SlackIntegration.Services
{
    public static class Configuration
    {
        public static string SlackAccessToken
            => System.Environment.GetEnvironmentVariable("SlackAccessToken");

        public static string SlackVerificationToken
            => System.Environment.GetEnvironmentVariable("SlackVerificationToken");
    }
}