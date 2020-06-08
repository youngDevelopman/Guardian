using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.ResourceService
{
    public class UpdateSegment
    {
        public string ResourceName { get; set; }

        public string BasePath { get; set; }

        public bool RequiresAuthentication { get; set; }
    }
}
