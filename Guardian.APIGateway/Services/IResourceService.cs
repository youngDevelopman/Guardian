using Guardian.APIGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway.Services
{
    public interface IResourceService
    {
        Task<ResourceServiceResponse> GetResources(string url);
    }
}
