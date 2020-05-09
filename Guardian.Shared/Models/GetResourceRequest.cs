using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models
{
    public class GetResourceRequest
    {
        public string Domain { get; set; }

        public string RelativePath { get; set; }
    }
}
