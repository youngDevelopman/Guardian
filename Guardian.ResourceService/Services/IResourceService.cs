using Guardian.ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public interface IResourceService
    {
        /// <summary>
        /// Retrieve UserPoolId
        /// </summary>
        /// <param name="domain">Domain of the resource.</param>
        /// <returns>Id of the resource.</returns>
        Task<DomainUserPool> GetUserPoolFromDomain(string domain);

        /// <summary>
        /// Retrieves all API Gateway resources for the given base path
        /// </summary>
        /// <param name="request">Request obj that contains information about source base path and relative path.</param>
        /// <returns>All resources that match to the given base path.</returns>
        Task<IEnumerable<Resource>> GetResources(ResourceServiceRequest request);
        
        /// <summary>
        /// Generates proxy for given request
        /// </summary>
        /// <param name="request">Request obj that contains information about source base path and relative path.</param>
        /// <returns></returns>
        Task<ResourceServiceResponse> GetResource(ResourceServiceRequest request);
    }
}
