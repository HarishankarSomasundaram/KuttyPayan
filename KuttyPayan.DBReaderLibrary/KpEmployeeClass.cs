using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuttyPayan.DBReaderLibrary
{
    public class KpEmployeeClass
    {
        static string connectionString = "mongodb://localhost";
        static MongoClient client = new MongoClient(connectionString);
        static MongoServer server = client.GetServer();
        static MongoDatabase database = server.GetDatabase("test");
        public string[] Tokenizer(string SearchInput)
        {
            return SearchInput.Trim().Split(' ');

        }
        public List<EmployeeSchema> MatchSchema(string[] SearchTokens)
        {
            int SearchTokenLength = SearchTokens.Length;
            MongoCollection<EmployeeSchema> collection = database.GetCollection<EmployeeSchema>("KPEmployeeSchema");
            var query = Query<EmployeeSchema>.EQ(e => e.Length, SearchTokenLength.ToString());

            List<EmployeeSchema> EmployeeSchemaList = collection.Find(query).ToList();
            if (EmployeeSchemaList.Count > 0)
            {
                return EmployeeSchemaList;
            }
            else
            {
                return null;
            }
        }
        public Dictionary<string, string> ColumnLikelihood(string SchemaName, string SchemaCoumnName, string InputCoumnName, int ColumnPosition)
        {
            MongoCollection<EmployeeSchema> collection = database.GetCollection<EmployeeSchema>("KPEmployeeSchema");
            var query = Query<EmployeeSchema>.EQ(e => e.Name, SchemaName);
            EmployeeSchema objEmployeeSchema = collection.Find(query).FirstOrDefault();
            string ReferenceColumn = string.Empty;
            Dictionary<string, string> ColumnReferencePair = new Dictionary<string, string>();

            if (objEmployeeSchema != null)
            {
                List<string[]> ColummReferenceTableArray = objEmployeeSchema.ReferenceTable;
                string ReferenceTable = ColummReferenceTableArray[ColumnPosition][1];
                if (ReferenceTable != "KPPassThroughDictionary")
                {


                    MongoCollection<EmployeeReference> ReferenceTablecollection = database.GetCollection<EmployeeReference>(ReferenceTable);
                    var EmployeeQuery = Query<EmployeeReference>.EQ(e => e.Key, InputCoumnName);
                    EmployeeReference objEmployeeReference = ReferenceTablecollection.Find(EmployeeQuery).FirstOrDefault();
                    if (objEmployeeReference != null)
                    {

                        ReferenceColumn = objEmployeeReference.Reference;
                    }

                    ColumnReferencePair.Add(objEmployeeSchema.Data[ColumnPosition][1], ReferenceColumn);
                }
                else
                {
                    ColumnReferencePair.Add(objEmployeeSchema.Data[ColumnPosition][1], InputCoumnName);
                }
            }
            return ColumnReferencePair;
        }
        //public string SchemaValidator(string SchemaName, string ColumnPosition)
        //{

        //}
    }
}
