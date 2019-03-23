using System;
using System.Collections.Generic;

namespace SlackIntegration.DTO.Slack
{
    public class Payload
    {
        public string type { get; set; }
        public List<Action> actions { get; set; }
        public string callback_id { get; set; }
        public Team team { get; set; }
        public Channel channel { get; set; }
        public User user { get; set; }
        public string action_ts { get; set; }
        public string message_ts { get; set; }
        public string attachment_id { get; set; }
        public string token { get; set; }
        public bool is_app_unfurl { get; set; }
        public string response_url { get; set; }
        public string trigger_id { get; set; }
        public Submission submission { get; set; }

        public Payload()
        {
            actions = new List<Action>();
            submission = new Submission();
        }
    }
}