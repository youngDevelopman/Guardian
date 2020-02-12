using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guardian.APIGateway
{
    public class Router
    {

        public List<Route> Routes { get; set; }
        public DestinationResource AuthenticationService { get; set; }


        public Router(string routeConfigFilePath)
        {
            dynamic router = JsonLoader.LoadFromFile<dynamic>(routeConfigFilePath);

            Routes = JsonLoader.Deserialize<List<Route>>(Convert.ToString(router.routes));
            AuthenticationService = JsonLoader.Deserialize<DestinationResource>(Convert.ToString(router.authenticationService));

        }
    }
}
