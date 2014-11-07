using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuttyPayan.MongodbLibrary;
using KuttyPayan.DBReaderLibrary;
using KuttyPayan.SqlLibrary;

namespace KuttyPayan.SchemaImplementerLibrary
{
    public class KPEmployeeSchemaImplementerClass
    {
        KuttyPayanMongodbClass KPDBObj = new KuttyPayanMongodbClass();
        KPSqlClass KPSql = new KPSqlClass();
        public string EmployeeSchemaImplementerMethod(SchemaEntityClass SchemaName)
        {
            List<WordSchemaReferenceValueClass> objWordSchemalist = SchemaName.WordSchemaReferenceValueCollection;
            int WordCount = SchemaName.WordCount;
            string SchemaMap = SchemaName.SchemaName;
            string[] SchemaReferenceArray = SchemaName.WordSchemaReferenceValueCollection.Select(a => a.SchemaReference).ToArray();
            string[] SchemaValueArray = SchemaName.WordSchemaReferenceValueCollection.Select(a => a.SchemaValue).ToArray();
            //var EmployeeSchema = KPDBObj.KPFindEmployeeSchemaMethod(SchemaName);
            List<SchemaMap> SchemaList = KPDBObj.KPEmployeeSchemaImplementerMethod();
            int FoundSchemaIndex = -1;
            for (int i = 0; i < SchemaList.Count; i++)
            {
                if (SchemaList[i].MappedSchema == SchemaMap)
                {
                    int count = 0;
                    for (int j = 0; j < SchemaReferenceArray.Count(); j++)
                    {
                        if (SchemaReferenceArray[j] == SchemaList[i].column[j])
                        {
                            count++;
                        }
                        else if(SchemaValueArray[j]==SchemaList[i].column[j])
                        {
                            count++;
                        }
                    }
                    if (SchemaReferenceArray.Count() == count)
                    {
                        FoundSchemaIndex = i;
                    }
                }

            }
            if (FoundSchemaIndex >= 0)
            {
                if( SchemaList[FoundSchemaIndex].group== "sql")
                {
                    KPSql.SqlSchemaParsermethod(SchemaList[FoundSchemaIndex], SchemaReferenceArray, SchemaValueArray);
                }
                string[] operation = SchemaList[FoundSchemaIndex].operation;
                return null;
            }
            else
            {
                return null;
            }
            //SchemaMap objSchemaMap = SchemaList.Where(a => a.MappedSchema == SchemaMap && a.columns == WordArray).All();
            //var EmployeeList = KPDBObj.KPEmployeeSchemaImplementerMethod("Employee");
            //CRUDSchema objCRUDSchema = FindCRUDSchema(EmployeeSchema, SchemaName);
 
        }
        public CRUDSchema FindCRUDSchema(EmployeeSchema SchemaName, SchemaEntityClass SchemaEntity)
        {
            return KPDBObj.KPFindCRUDSchemaMethod(SchemaName, SchemaEntity);
        }
    }
}
