using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FooAirline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Add Passenger",
                url: "passenger/add",
                defaults: new { controller = "Home", action = "AddPassenger" }
            );

            routes.MapRoute(
                name: "Add Flight",
                url: "flight/add",
                defaults: new { controller = "Home", action = "AddFlight" }
            );

            routes.MapRoute(
                name: "Get Flight",
                url: "flight/{id}",
                defaults: new { controller = "Home", action = "Flight" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Error",
                url: "Error",
                defaults: new { controller = "Home", action = "Error" }
            );
        }
    }
}
