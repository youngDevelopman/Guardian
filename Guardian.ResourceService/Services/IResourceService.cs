using Guardian.ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceModel>> GetResources(ResourceServiceRequest request);
        Task<ResourceModel> GetResource(ResourceServiceRequest request);
    }
}
