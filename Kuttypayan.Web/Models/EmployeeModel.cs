using KuttyPayan.SchemaEvaluatorLibrary;
using KuttyPayan.SchemaImplementerLibrary;
using KuttyPayan.DBReaderLibrary;
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
        public List<EmployeeSample> EmployeeList { get; set; }
    }
    public class EmployeeSearchLogic
    {
        //public Dictionary<string, List<Dictionary<string, string>>> SearchModel(string SearchInput)
        public string SearchModel(string SearchInput)
        {
            string Result = string.Empty;

            KuttyPayanMongodbClass KPdbObj = new KuttyPayanMongodbClass();
            var SearchResult = KPdbObj.KPEmployeeSearchMethod(SearchInput);
            KPEmployeeSchemaEvaluatorClass SchemaEvaluator = new KPEmployeeSchemaEvaluatorClass();
            SchemaEntityClass SchemaResult = SchemaEvaluator.EmployeeSchemaEvaluatorMethod(SearchResult, SearchInput);

            KPEmployeeSchemaImplementerClass objEmployeeImplementor = new KPEmployeeSchemaImplementerClass();
            List<EmployeeSample> EmployeeList = new List<EmployeeSample>();
            if (SchemaResult != null)
            {
                Result = objEmployeeImplementor.EmployeeSchemaImplementerMethod(SchemaResult);
                //EmployeeList = objEmployeeImplementor.EmployeeSchemaImplementerMethod(SchemaResult);
            }
            else
            {
                Result = "Schema Not Found";
            }
            return Result;

            // return SearchResult;
            //return EmployeeList;
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