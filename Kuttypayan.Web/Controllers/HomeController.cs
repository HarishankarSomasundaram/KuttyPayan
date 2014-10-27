using Kuttypayan.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kuttypayan.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            DictionaryModel objDctionaryModel = new DictionaryModel();

            return View(objDctionaryModel);
        }
        [HttpPost]
        public ActionResult Index(DictionaryModel objDctionaryModel)
        {
            if (ModelState.IsValid)
            {
                SearchLogic objSearchLogic = new SearchLogic();
                objDctionaryModel.SearchResult = objSearchLogic.SearchModel(objDctionaryModel.Search);
                return View(objDctionaryModel);
                //return PartialView("WordListPartial", objDctionaryModel);
            }
            return View(objDctionaryModel);
        }
    }
}