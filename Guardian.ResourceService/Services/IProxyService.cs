using Guardian.ResourceService.Models;
using Guardian.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.ResourceService.Services
{
    public interface IProxyService
    {
        Task<ProxyResource> GenerateProxy(string domain, string relativePath);
    }
}
