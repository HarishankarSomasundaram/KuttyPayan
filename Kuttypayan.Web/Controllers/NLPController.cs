using Kuttypayan.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KuttyPayan.NLP;
using KuttyPayan.DBReaderLibrary;

namespace Kuttypayan.Web.Controllers
{
    public class NLPController : Controller
    {
        //
        // GET: /NLP/
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

                KuttyPayanPosTaggerClass objTagger = new KuttyPayanPosTaggerClass();
                Dictionary<string, string> POSText = objTagger.PosTaggerMethod(objDctionaryModel.Search);
                //objDctionaryModel.NLPSearchResult = objTagge

                KuttyPayan.MongodbLibrary.KuttyPayanMongodbClass ObjClass = new KuttyPayan.MongodbLibrary.KuttyPayanMongodbClass();
                KuttyPayan.DBReaderLibrary.KuttyPayanDbReaderClass ObjDbRead = new KuttyPayan.DBReaderLibrary.KuttyPayanDbReaderClass();
                List<SearchInputTags> KPTags = ObjDbRead.SearchTextToDBMethod(POSText, objDctionaryModel.Search);
                bool status = ObjClass.KuttyPayanSearchTagInsert(KPTags);


                SearchLogic objSearchLogic = new SearchLogic();
                objDctionaryModel.SearchResult = objSearchLogic.SearchModel(objDctionaryModel.Search);
                return View(objDctionaryModel);
                //return PartialView("WordListPartial", objDctionaryModel);
            }
            return View(objDctionaryModel);
        }
    }
}