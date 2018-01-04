using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace wikiPr
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("home", "index", new { controller = "home", action = "afficher" });

           // routes.MapRoute("Home", "modifier", new { controller = "Home", action = "modifier" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{titre}",
                defaults: new { controller = "Home", action = "Index", titre = UrlParameter.Optional }
            );
        }
    }
}
