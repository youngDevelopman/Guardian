using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Guardian.Shared.Models
{
    public class GetResourceRequest
    {
        [Required]
        public string Domain { get; set; }

        [Required]
        public string RelativePath { get; set; }
    }
}
