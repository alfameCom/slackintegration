using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlackIntegration.Data
{
    public partial class SlackDebugMessages
    {
        public int Id { get; set; }
        public DateTime Received { get; set; }
        public string Message { get; set; }
    }
}
