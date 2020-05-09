using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.AuthorizationService.Models
{
    public class PoolUser
    {
        public Guid PoolUsersId { get; set; }

        public Guid UserId { get; set; }

        public Guid UserPoolId { get; set; }
    }
}
