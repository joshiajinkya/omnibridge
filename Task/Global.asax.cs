using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Task
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
            //if (ex is HttpAntiForgeryException)
            //{
            //    Response.Clear();
            //Helper hp = new Helper();
            //hp.LogException(ex);
            //SendMails(ex.Message, "None", "None");
            Server.ClearError(); //make sure you log the exception first /fonts/glyphicons-halflings-regular.woff' was not found or does not implement IController."}
            Response.Redirect("~/Views/Shared/_Errorpage", true);
            //}
        }
    }
}
