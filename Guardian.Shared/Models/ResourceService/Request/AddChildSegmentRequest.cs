using Guardian.ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.ResourceService.Request
{
    public class AddChildSegmentRequest
    {
        public string ParentSegmentId { get; set; }

        public AddResourceSegment Segment { get; set; }
    }
}
