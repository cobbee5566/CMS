using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
            //http://www.dotnettricks.com/learn/mvc/aspnet-mvc-4-performance-optimization-with-bundling-and-minification
            //Bundling and minification doesn't work in debug mode. So to enable this featues you need to add below line of code with in Application_Start event of Global.asax.
        }
    }
}
