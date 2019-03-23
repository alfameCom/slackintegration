using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlackIntegration.Data
{
    public partial class SlackSuccess
    {
        public SlackSuccess()
        {
            SlackSuccessReceiver = new HashSet<SlackSuccessReceiver>();
        }

        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Praiser { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime SuccessDate { get; set; }

        [InverseProperty("Success")]
        public virtual ICollection<SlackSuccessReceiver> SlackSuccessReceiver { get; set; }
    }
}
