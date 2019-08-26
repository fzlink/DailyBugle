using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DailyBugle.Controllers;

namespace DailyBugle
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "FrontEndPage",
                url: "FrontEnd/{controller}/{action}/{id}",
                defaults: new { controller = "HomePage", action = "Index"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional }
                
            );


            routes.MapRoute(
                name: "AdminHomePage",
                url: "Admin/AdminPage/{action}/{id}",
                defaults: new {controller = "AdminPage", action= "Index", id= UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "EditorHomePage",
                url: "Editor/EditorPage/{action}/{id}",
                defaults: new {controller = "EditorPage", action = "Index", id =UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "UserHomePage",
                url: "User/{action}/{id}",
                defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "AdminNewsPage",
                url: "Admin/News/Index/{id}",
                defaults: new {area = "Admin", controller = "News", action = "Index", id = UrlParameter.Optional}
                );

            routes.MapRoute(
                name: "EditorNewsPage",
                url: "Editor/News/Index/{id}",
                defaults: new {area = "Editor", controller = "News", action = "Index", id = UrlParameter.Optional}
                );
        }
    }
}
