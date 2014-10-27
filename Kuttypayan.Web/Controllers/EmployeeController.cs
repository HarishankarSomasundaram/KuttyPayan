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
                var SearchResponds = objSearchLogic.SearchModel(objEmpModel.Search);
                
                if (SearchResponds!=null)
                {
                    objEmpModel.SearchResult = SearchResponds;
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