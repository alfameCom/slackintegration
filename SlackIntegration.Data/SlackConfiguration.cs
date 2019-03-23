using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SlackIntegration.Data
{
    public partial class SlackConfiguration
    {
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
