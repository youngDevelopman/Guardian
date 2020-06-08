using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.ResourceService
{
    public class GetSegment
    {
        public string SegmentId { get; set; }

        public string ResourceName { get; set; }

        public string BasePath { get; set; }

        public bool RequiresAuthentication { get; set; }

        public List<GetSegment> ChildSegments { get; set; }
    }
}
