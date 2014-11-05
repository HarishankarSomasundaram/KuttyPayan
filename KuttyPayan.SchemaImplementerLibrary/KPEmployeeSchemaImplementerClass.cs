using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuttyPayan.MongodbLibrary;
using KuttyPayan.DBReaderLibrary;

namespace KuttyPayan.SchemaImplementerLibrary
{
    public class KPEmployeeSchemaImplementerClass
    {
        KuttyPayanMongodbClass KPDBObj = new KuttyPayanMongodbClass();
        public List<EmployeeSample> EmployeeSchemaImplementerMethod(SchemaEntityClass SchemaName)
        {
            List<WordSchemaReferenceValueClass> objWordSchemalist = SchemaName.WordSchemaReferenceValueCollection;
            int WordCount = SchemaName.WordCount;
            string SchemaMap = SchemaName.SchemaName;
           string[] WordArray = SchemaName.WordSchemaReferenceValueCollection.Select(a => a.SchemaReference).ToArray();

            //var EmployeeSchema = KPDBObj.KPFindEmployeeSchemaMethod(SchemaName);
            List<SchemaMap> SchemaList = KPDBObj.KPEmployeeSchemaImplementerMethod(SchemaMap);
            //SchemaMap objSchemaMap = SchemaList.Where(a => a.MappedSchema == SchemaMap && a.columns == WordArray).All();
            var EmployeeList = KPDBObj.KPEmployeeSchemaImplementerMethod("Employee");
            //CRUDSchema objCRUDSchema = FindCRUDSchema(EmployeeSchema, SchemaName);

            return null;
        }
        public CRUDSchema FindCRUDSchema(EmployeeSchema SchemaName, SchemaEntityClass SchemaEntity)
        {
            return KPDBObj.KPFindCRUDSchemaMethod(SchemaName, SchemaEntity);
        }
    }
}
