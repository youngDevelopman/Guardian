using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway
{
    public class Route
    {
        public string Endpoint { get; set; }
        public DestinationResource Destination { get; set; }
    }
}
