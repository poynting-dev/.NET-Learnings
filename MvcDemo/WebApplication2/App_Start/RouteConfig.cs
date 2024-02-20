﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication2.Controllers;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                   "Default", // Route name  
                    "{controller}/{action}/{id}", // URL with parameters  
                    new { controller = "Home", action = "about", id = UrlParameter.Optional } // Parameter defaults  
               );
        }
    }
}