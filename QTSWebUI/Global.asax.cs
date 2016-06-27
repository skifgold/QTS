//#define DB_INITIALIZE
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using QTSWebUI.Attributes;

namespace QTSWebUI
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
#if DB_INITIALIZE
            System.Data.Entity.Database.SetInitializer(new DbInit());
#endif
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalFilters.Filters.Add(new ActionObserver());
            GlobalConfiguration.Configuration.Filters.Add(new ActionApiObserver());
        }
    }
}