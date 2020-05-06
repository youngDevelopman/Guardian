using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService.Models
{
    public class UserPoolDomain
    {
        public Guid UserPoolDomainId { get; set; }

        public Guid UserPoolId { get; set; }

        public string Domain { get; set; }
    }
}
