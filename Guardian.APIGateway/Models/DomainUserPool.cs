using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Models
{
    public class DomainUserPool
    {
        public string Domain { get; set; }

        public Guid UserPoolId { get; set; }
    }
}
