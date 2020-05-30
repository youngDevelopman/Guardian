using Guardian.ResourceService.Models;

namespace Guardian.Shared.Models
{
    public class AddRootSegmentRequest
    {
        public string GatewayId { get; set; }

        public ResourceSegment Segment { get; set; }
    }
}
