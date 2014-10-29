using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kuttypayan.Web.Models;

namespace Kuttypayan.Web.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            EmployeeModel objEmpModel = new EmployeeModel();
            return View(objEmpModel);
        }
        [HttpPost]
        public ActionResult Index(EmployeeModel objEmpModel)
        {
            if (ModelState.IsValid)
            {
                EmployeeSearchLogic objSearchLogic = new EmployeeSearchLogic();
              //  Dictionary<stbring, List<Dictionary<string, string>>>
                var EmployeeList = objSearchLogic.SearchModel(objEmpModel.Search);

                if (EmployeeList != null)
                {
                    objEmpModel.EmployeeList = EmployeeList;
                }
                else
                {
                    objEmpModel.SearchResult = null;
                }
                return View(objEmpModel);
            }
            return View(objEmpModel);
        }
    }
}