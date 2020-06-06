using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.AuthorizationService
{
    public class UserPool
    {
        public Guid UserPoolId { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
