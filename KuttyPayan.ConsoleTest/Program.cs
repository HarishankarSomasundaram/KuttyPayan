using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuttyPayan.DBReaderLibrary;

namespace KuttyPayan.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            KuttyPayan.MongodbLibrary.KuttyPayanMongodbClass ObjClass = new MongodbLibrary.KuttyPayanMongodbClass();

            KuttyPayan.DBReaderLibrary.KuttyPayanDbReaderClass ObjDbRead = new DBReaderLibrary.KuttyPayanDbReaderClass();
            //List<Dictionary> KPDictionary = ObjDbRead.FileReadMethod();
            //ObjClass.KuttyPayanMethod(KPDictionary);
            //List<POSTags> KPTags = ObjDbRead.TagReadMethod();
            //ObjClass.KuttyPayanMethod(KPTags);
            //List<EmployeeSample> Employees = ObjDbRead.InsertEmployees();
            //bool Status = ObjClass.KuttyPayanEmployeeInsert(Employees);

            EmployeeSchema EmpSchema = new EmployeeSchema();
            EmpSchema.Name = "update";
            EmpSchema.Length = "2";
            EmpSchema.Type = "DB";
            string[] Column1 = { "column1", "KPActionDictionary" };
            string[] Column2 = { "column2", "KPTableDictionary" };
            EmpSchema.ReferenceTable = new List<string[]>();
            EmpSchema.ReferenceTable.Add(Column1);
            EmpSchema.ReferenceTable.Add(Column2);
            EmpSchema.Data = new List<string[]>();
            EmpSchema.Data.Add(new string[] { "column1", "action" });
            EmpSchema.Data.Add(new string[] { "column2", "tablename" });
            bool Status = ObjClass.KuttyPayanEmployeeSchemaInsert(EmpSchema); 
        }
    }
}
