﻿using Guardian.ResourceService.Models;
using Guardian.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public interface IResourceService
    {        
        /// <summary>
        /// Generates proxy for given request
        /// </summary>
        /// <param name="request">Request obj that contains information about source base path and relative path.</param>
        /// <returns></returns>
        Task<GetResourceResponse> GetResource(GetResourceRequest request);

        /// <summary>
        /// Generates proxy for given request
        /// </summary>
        /// <param name="request">Request obj that contains information about source base path and relative path.</param>
        /// <returns></returns>
        Task<GetGatewaysResponse> GetGateways();

        /// <summary>
        /// Generates proxy for given request
        /// </summary>
        /// <param name="request">Request obj that contains information about source base path and relative path.</param>
        /// <returns></returns>
        Task<GetGatewayResponse> GetGateway(string gatewayId);

        /// <summary>
        /// Generates proxy for given request
        /// </summary>
        /// <param name="request">Request obj that contains information about source base path and relative path.</param>
        /// <returns></returns>
        Task<bool> UpdateGateway(UpdateGatewayRequest request);

        Task<Resource> AddRootSegment(string gatewayId, AddRootSegmentRequest request);
    }
}
