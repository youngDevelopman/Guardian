using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.ResourceService
{
    public class UpdateResourceSegment
    {
        public string SegmentId { get; set; }

        public string ResourceName { get; set; }

        public string BasePath { get; set; }

        public bool RequiresAuthentication { get; set; }

        public List<UpdateResourceSegment> ChildSegments { get; set; }
    }
}
