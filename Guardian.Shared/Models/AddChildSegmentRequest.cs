using Guardian.ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models
{
    public class AddChildSegmentRequest
    {
        public string ParentSegmentId { get; set; }

        public ResourceSegment Segment { get; set; }
    }
}
