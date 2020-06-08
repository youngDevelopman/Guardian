using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.ResourceService
{
    public class UpdateResource
    {
        public Guid UserPoolId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Domain { get; set; }
    }
}
