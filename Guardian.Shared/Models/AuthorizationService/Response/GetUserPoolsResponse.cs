using System;
using System.Collections.Generic;
using System.Text;

namespace Guardian.Shared.Models.AuthorizationService.Response
{
    public class GetUserPoolsResponse
    {
        public IEnumerable<UserPool> UserPools { get; set; }
    }
}
