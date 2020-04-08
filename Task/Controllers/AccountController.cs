using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.BAL;

namespace Task.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                model.roleid = 1;
                Login obj = new Login();
                //return RedirectToAction("index", "Home");
                var User = obj.MemberLogin(model);
                if (User != null)
                {
                    var id = Convert.ToInt32(User.Rows[0]["Result"]);
                    if (id > 0)
                    {
                        UserDetails md = new UserDetails();
                        md.firstname = User.Rows[0]["FirstName"].ToString();
                        md.lastname = User.Rows[0]["LastName"].ToString();
                        md.username = model.UserId;
                        Session["User"] = md;
                        return RedirectToAction("Index", "Employee");
                    }
                    else
                    {
                        ViewBag.Error = "The username or password is incorrect.";
                    }
                }
                else
                {
                    ViewBag.Error = "The username or password is incorrect.";
                }
            }
            else
            {
                ViewBag.Error = "The username or password is incorrect.";
            }
            //return View(User);
            return View();
        }

        public ActionResult LogOut()
        {
            Response.StatusCode = 303;

            //  AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Create", "Employee");
        }
    }
}