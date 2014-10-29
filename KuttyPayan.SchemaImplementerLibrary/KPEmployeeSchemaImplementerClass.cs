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
            var EmployeeSchema = KPDBObj.KPFindEmployeeSchemaMethod(SchemaName);
            var EmployeeList = KPDBObj.KPEmployeeSchemaImplementerMethod("Employee");
            CRUDSchema objCRUDSchema = FindCRUDSchema(EmployeeSchema, SchemaName);

            return EmployeeList;
        }
        public CRUDSchema FindCRUDSchema(EmployeeSchema SchemaName, SchemaEntityClass SchemaEntity)
        {
            return KPDBObj.KPFindCRUDSchemaMethod(SchemaName, SchemaEntity);
        }
    }
}
