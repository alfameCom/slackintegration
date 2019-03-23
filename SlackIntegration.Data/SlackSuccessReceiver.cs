using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlackIntegration.Data
{
    public partial class SlackSuccessReceiver
    {
        public long Id { get; set; }
        public long SuccessId { get; set; }
        [Required]
        [StringLength(50)]
        public string Receiver { get; set; }

        [ForeignKey("SuccessId")]
        [InverseProperty("SlackSuccessReceiver")]
        public virtual SlackSuccess Success { get; set; }
    }
}
