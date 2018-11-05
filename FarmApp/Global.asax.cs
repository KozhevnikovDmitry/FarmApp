using FarmApp.Util;
using System;
/*
 * CR-1 - remove redundant using
 */
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FarmApp
{
    /*
     * CR-1 - add a comment to class
     */
    public class MvcApplication : System.Web.HttpApplication
    {
        /*
         * CR-1 - add comment to method
         */
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainer();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        /*
         * CR-1
         * Add comment to method
         * Add logging of application error
         */
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            Response.Redirect("/Content/ExceptionFound.html");
        }
    }
}
