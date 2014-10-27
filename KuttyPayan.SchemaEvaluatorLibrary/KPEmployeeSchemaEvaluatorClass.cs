using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuttyPayan.MongodbLibrary;
using KuttyPayan.DBReaderLibrary;

namespace KuttyPayan.SchemaEvaluatorLibrary
{
    public class KPEmployeeSchemaEvaluatorClass
    {
        public string EmployeeSchemaEvaluatorMethod(Dictionary<string, List<Dictionary<string, string>>> SearchResult, string SearchInput)
        {
            string result = string.Empty;
            if (SearchResult != null)
            {
                List<SchemaEntityClass> SchemaEntityList = new List<SchemaEntityClass>();
                SchemaEntityList = EmployeeSchemaEntityParser(SearchResult, SearchInput);
               
                KuttyPayanMongodbClass KPDBObj = new KuttyPayanMongodbClass();
                bool IsInserted = KPDBObj.KPEmployeeSchemaInsertMethod(SchemaEntityList);
                if (IsInserted)
                {
                    result = EmployeeSchemaIdentifier(SchemaEntityList);
                }
               

            }
            else
            {
                result = "sorry, could't understand your query";
            }
            return result;
        }
        public string EmployeeSchemaIdentifier(List<SchemaEntityClass> SchemaEntityList)
        {
            var maxValue = SchemaEntityList.Max(x => x.WordSchemaMatchCount);

            var value = SchemaEntityList.FirstOrDefault().WordCount;
            if (value == maxValue)
            { 
                var SchemaMatch = SchemaEntityList.First(x => x.WordSchemaMatchCount == maxValue).SchemaName;
                string ResultStr = "you are trying to perform an action based on the schema - " + SchemaMatch.ToUpper() + " !";
                return ResultStr;
            }
            else
            {

                double percentageValue = (((double)maxValue / (double)value) * 100);
                percentageValue= Math.Round(percentageValue, 1);
                var SchemaMatchList = SchemaEntityList.Where(x => x.WordSchemaMatchCount == maxValue).ToList();
                StringBuilder ResultSB = new StringBuilder();
                ResultSB.Append("given query is "+percentageValue+"% matches with the schema - ");
                foreach (SchemaEntityClass entity in SchemaMatchList)
                {
                    ResultSB.Append(entity.SchemaName.ToUpper() + "; ");

                }
                ResultSB.Append(Environment.NewLine);
                ResultSB.Append("please modify your query or wait until next week end :)");
                return ResultSB.ToString();
            }


        }
        public List<SchemaEntityClass> EmployeeSchemaEntityParser(Dictionary<string, List<Dictionary<string, string>>> SearchResult, string SearchInput)
        {
            List<SchemaEntityClass> SchemaEntityList = new List<SchemaEntityClass>();
            string[] SearchInputWords = SearchInput.Trim().Split(' ');


            // Iterate schema collection
            foreach (KeyValuePair<string, List<Dictionary<string, string>>> Item in SearchResult)
            {
                SchemaEntityClass objSchemaEntity = new SchemaEntityClass();
                objSchemaEntity.SchemaName = Item.Key;
                objSchemaEntity.InputSearch = SearchInput;
                objSchemaEntity.WordCount = Item.Value.Count;
                int WordSchemaMatchCount = 0;
              
                // Iterate List of columns
                foreach (Dictionary<string, string> Columns in Item.Value)
                {

                    DateTime CurrentDateTime = DateTime.Now;
                    List<WordSchemaReferenceValueClass> WordSchemaList = new List<WordSchemaReferenceValueClass>();
                    //Iterate dictionary - column : action, value pair
                    //bug
                    for (int i = 0; i < Columns.Count; i++)
                    {
                        objSchemaEntity.WordSchemaReferenceValueCollection = new List<WordSchemaReferenceValueClass>();
                        WordSchemaReferenceValueClass objWordSchema = new WordSchemaReferenceValueClass();

                        if (Columns.Values.ElementAt(0) != string.Empty)
                        {
                            WordSchemaMatchCount++;
                        }
                        objWordSchema.Word = SearchInputWords[i];
                        objWordSchema.SchemaReference = Columns.Keys.ElementAt(i);
                        objWordSchema.SchemaValue = Columns.Values.ElementAt(i);
                      
                        WordSchemaList.Add(objWordSchema);
                    }
                    objSchemaEntity.WordSchemaMatchCount = WordSchemaMatchCount;
                    objSchemaEntity.WordSchemaReferenceValueCollection = WordSchemaList;
                }
                SchemaEntityList.Add(objSchemaEntity); 
            }
            return SchemaEntityList;
        }
    }
}
