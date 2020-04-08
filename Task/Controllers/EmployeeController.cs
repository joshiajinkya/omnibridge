using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.BAL;
using System.Data;
using Task.Common;

namespace Task.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee

        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public string BindListGrid()
        {
            try
            {
                employeeService obj = new employeeService();
                empmdl mdl = new empmdl();
                mdl.action = "L";

                var modelList = obj.emp_crud(mdl);

                return Newtonsoft.Json.JsonConvert.SerializeObject(modelList);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Create()
        {
            return View();
        }


        public string Save(empmdl mdl)
        {
            try
            {
                employeeService md = new employeeService();
                mdl.action = "C";
                mdl.recorddt = DateTime.Now.ToString("dd/MMM/yyyy");
                var result = md.emp_crud(mdl);
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);

            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(ex.Message);
            }

        }


        public string ChkName(empmdl mdl)
        {
            try
            {
                employeeService md = new employeeService();
                mdl.action = "R";
                var result = md.emp_crud(mdl);
                return Newtonsoft.Json.JsonConvert.SerializeObject(result);

            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(ex.Message);
            }

        }

        public void ExportExclVoucher(empmdl rMd)
        {
            employeeService md = new employeeService();
            empmdl mdl = new empmdl();
            mdl.action = "L";

            var modelList = md.emp_export();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=Voucher.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            //WriteTsv(data, Response.Output);
            CommonFunctions.WriteHtmlTable(modelList, Response.Output);
            Response.End();
        }
    }
}