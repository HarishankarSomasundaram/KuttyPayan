using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuttyPayan.DBReaderLibrary;
using System.Text.RegularExpressions;

namespace KuttyPayan.SqlLibrary
{
    public class KPSqlClass
    {
        public string SqlSchemaParsermethod(SchemaMap Schema, string[] SchemaReferenceArray, string[] SchemaValueArray, string SchemaMap)
        {
            string[] ParseOperation = Schema.operation;
            StringBuilder sb = new StringBuilder();

            foreach (string unit in ParseOperation)
            {

                if (Regex.IsMatch(unit, @"^\d+$"))
                {
                    string value = SchemaValueArray[Convert.ToInt32(unit)];
                    sb.Append(value);
                    sb.Append(" ");
                }
                else
                {
                    sb.Append(unit);
                    sb.Append(" ");
                }
            }
            string ParsedSrting = sb.ToString().Trim();
            string Result = string.Empty;
            //if (SchemaMap == "select")
            //{
            KPSqlImplementer objImplementor = new KPSqlImplementer();
            Result = objImplementor.SqlImplementerMethod(ParsedSrting);
            //}
            //else if (SchemaMap == "selectfew")
            // {
            //     KPSqlImplementer objImplementor = new KPSqlImplementer();
            //     Result = objImplementor.SqlImplementerMethod(ParsedSrting);
            // }
            return Result;
        }
    }
}
