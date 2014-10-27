using KuttyPayan.SchemaEvaluatorLibrary;
using KuttyPayan.MongodbLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kuttypayan.Web.Models
{
    public class EmployeeModel
    {
        [Required(ErrorMessage = "Please type something")]
        public string Search { get; set; }
        //public Dictionary<string, List<Dictionary<string, string>>> SearchResult { get; set; }
        public string SearchResult { get; set; }
    }
    public class EmployeeSearchLogic
    {
        //public Dictionary<string, List<Dictionary<string, string>>> SearchModel(string SearchInput)
        public string SearchModel(string SearchInput)
        {
            KuttyPayanMongodbClass KPdbObj = new KuttyPayanMongodbClass();

            var SearchResult = KPdbObj.KPEmployeeSearchMethod(SearchInput);

            KPEmployeeSchemaEvaluatorClass SchemaEvaluator = new KPEmployeeSchemaEvaluatorClass();
            string Result = SchemaEvaluator.EmployeeSchemaEvaluatorMethod(SearchResult, SearchInput);

           // return SearchResult;
            return Result;
        }
        //public List<List<string>> SchemaAnalysis(List<List<string>> SearchResult)
        //{
        //    SearchResult = new List<List<string>>();
        //    foreach (List<string> InnerList in SearchResult)
        //    {
        //        foreach (string innerText in InnerList)
        //        {

        //        }
        //    }
        //}
    }
}