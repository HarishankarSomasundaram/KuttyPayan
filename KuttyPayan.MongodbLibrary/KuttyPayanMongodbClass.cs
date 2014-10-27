using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using KuttyPayan.DBReaderLibrary;
namespace KuttyPayan.MongodbLibrary
{
    public class KuttyPayanMongodbClass
    {
        static string connectionString = "mongodb://localhost";
        static MongoClient client = new MongoClient(connectionString);
        static MongoServer server = client.GetServer();
        static MongoDatabase database = server.GetDatabase("test");
        MongoCollection<Dictionary> collection = database.GetCollection<Dictionary>("KPDictionary");
        public List<Dictionary> KuttyPayanSearchMethod(string SearchKey)
        {

            var query = Query<Dictionary>.EQ(e => e.Word, SearchKey.ToLower());
            //var query = Query<Dictionary>.Where(e => e.Word.StartsWith( SearchKey.ToLower()));
            List<Dictionary> DictObj = collection.Find(query).ToList();
            return DictObj;
        }
        public bool KuttyPayanSearchTagInsert(List<SearchInputTags> SearchInputTags)
        {
            try
            {
                MongoCollection<SearchInputTags> SearchTagscollection = database.GetCollection<SearchInputTags>("KPSearchTags");

                foreach (SearchInputTags Tag in SearchInputTags)
                {
                    SearchTagscollection.Insert(Tag);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool KuttyPayanEmployeeInsert(List<EmployeeSample> EmployeeList)
        {
            try
            {
                MongoCollection<EmployeeSample> Employeecollection = database.GetCollection<EmployeeSample>("Employee");

                foreach (EmployeeSample Employee in EmployeeList)
                {
                    Employeecollection.Insert(Employee);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool KuttyPayanEmployeeSchemaInsert(EmployeeSchema EmployeeSchema)
        {
            try
            {
                MongoCollection<EmployeeSchema> Employeecollection = database.GetCollection<EmployeeSchema>("KPEmployeeSchema");


                Employeecollection.Insert(EmployeeSchema);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public void KuttyPayanMethod(List<Dictionary> objDict)
        {
            //var connectionString = "mongodb://localhost";
            //var client = new MongoClient(connectionString);
            //var server = client.GetServer();
            //var database = server.GetDatabase("test");
            //var collection = database.GetCollection<Dictionary>("KPDictionary");

            foreach (Dictionary word in objDict)
            {
                collection.Insert(word);
            }
            //var entity = new Entity { Name = "Tom" };
            //collection.Insert(entity);
            //var id = entity.Id;

            //var query = Query<Entity>.EQ(e => e.Id, id);
            //entity = collection.FindOne(query);

            //entity.Name = "hari";
            //collection.Save(entity);

            //var update = Update<Entity>.Set(e => e.Name, "Harry");
            //collection.Update(query, update);

            //   collection.Remove(query);
        }
        public void KuttyPayanMethod(List<POSTags> objTags)
        {

            foreach (POSTags Tag in objTags)
            {
                collection.Insert(Tag);
            }

        }

        public bool KPEmployeeSchemaInsertMethod(List<SchemaEntityClass> EmployeeSchema)
        {

            try
            {
                MongoCollection<SchemaEntityClass> Employeecollection = database.GetCollection<SchemaEntityClass>("KPEmployeeSearchLog");

                foreach (SchemaEntityClass Entity in EmployeeSchema)
                {
                    Entity.SearchDateTime = DateTime.Now;
                    Employeecollection.Insert(Entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public Dictionary<string, List<Dictionary<string, string>>> KPEmployeeSearchMethod(string SearchKey)
        {
            KpEmployeeClass EmpObj = new KpEmployeeClass();
            string[] SearchTokens = EmpObj.Tokenizer(SearchKey);
            List<EmployeeSchema> ObjEmployeeSchema = EmpObj.MatchSchema(SearchTokens);
            //List<Dictionary<string, int>> ScemaLikelihoodList = new List<Dictionary<string, int>>();
            List<List<string>> ColumnBuilder = new List<List<string>>();
            Dictionary<string, List<Dictionary<string, string>>> SchemaColumReferencePair = new Dictionary<string, List<Dictionary<string, string>>>();


            if (ObjEmployeeSchema != null)
            {
                for (int i = 0; i < ObjEmployeeSchema.Count; i++)
                {
                    List<string> ColumnForCurrentSchema = new List<string>();
                    Dictionary<string, string> ColumnReferencePair = new Dictionary<string, string>();
                    List<Dictionary<string, string>> ColumnReferencePairList = new List<Dictionary<string, string>>();
                    for (int j = 0; j < ObjEmployeeSchema[i].Data.Count; j++)
                    {

                        ColumnReferencePair = EmpObj.ColumnLikelihood(ObjEmployeeSchema[i].Name, ObjEmployeeSchema[i].Data[j][1], SearchTokens[j], j);
                        ColumnReferencePairList.Add(ColumnReferencePair);
                    }
                    SchemaColumReferencePair.Add(ObjEmployeeSchema[i].Name, ColumnReferencePairList);

                    //foreach (String[] Column in ObjEmployeeSchema[i].Data)
                    //{
                    //    Dictionary<string, int> ColumnLikelihood = new Dictionary<string, int>();
                    //    ColumnForCurrentSchema.Add(EmpObj.ColumnLikelihood(ObjEmployeeSchema[i].Name, Column[1], SearchTokens[i], i));
                    //    //ColumnLikelihood.Add(Column[1], ColumnValue);
                    //    //ScemaLikelihoodList.Add(ColumnLikelihood);
                    //}
                    // ColumnBuilder.Add(ColumnForCurrentSchema);
                }
                return SchemaColumReferencePair;
            }
            else
            {
                return null;
            }

        }
    }

}
