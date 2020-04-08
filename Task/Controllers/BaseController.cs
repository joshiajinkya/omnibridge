using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Task.Models;

namespace Task.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Session["User"] = null;
            if (Session["User"] != null)
            {
                filterContext.Controller.ViewBag.User = User;
            }
            else
            {
                FormsAuthentication.SignOut();
            
                var returnUrl = filterContext.HttpContext.Request.RawUrl;
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult result = Json("SessionTimeout", "text/html");
                    filterContext.Result = result;
                }
                if (filterContext.HttpContext.Request.RequestType == "POST")
                {
                    returnUrl = filterContext.HttpContext.Request.UrlReferrer.PathAndQuery;
                   
                }
                filterContext.HttpContext.Response.StatusCode = 403;
                string rtn = returnUrl;
                filterContext.HttpContext.Response.StatusDescription = rtn;
                filterContext.Controller.ViewBag.User = null;
                
                RedirectToAction("Login", "Account", new { Area = "" });
              
            }
            base.OnActionExecuting(filterContext);
            
        }

        public new User User
        {
            get
            {
                return Session["User"] as User;
            }
            set
            {
                Session["User"] = value;
            }
        }
    }

    public class Err : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            //filterContext.Result = new ViewResult()
            var result = new ViewResult()
            {
                ViewName = "ErrorPage",
                //ViewBag="",
                ViewData = new ViewDataDictionary(model)
            };
            result.ViewBag.BreadCrumb1 = actionName;
            result.ViewBag.BreadCrumb2 = "Error";
            filterContext.Result = result;

        }
    }

}