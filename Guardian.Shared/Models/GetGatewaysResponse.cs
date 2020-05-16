using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models
{
    public class GetGatewaysResponse
    {
        public IEnumerable<GatewayShortInfo> Gateways { get; set; }
    }
}
