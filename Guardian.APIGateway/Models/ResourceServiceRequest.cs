﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Models
{
    public class ResourceServiceRequest
    {
        public string Domain { get; set; }
        public string RelativePath { get; set; }
    }
}
