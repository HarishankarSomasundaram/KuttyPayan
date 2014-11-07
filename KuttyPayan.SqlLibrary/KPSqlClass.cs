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
        public void SqlSchemaParsermethod(SchemaMap Schema, string[] SchemaReferenceArray, string[] SchemaValueArray)
        {
            string[] ParseOperation = Schema.operation;
            StringBuilder sb = new StringBuilder();

            foreach(string unit in ParseOperation)
            {
                
                if(Regex.IsMatch(unit, @"^\d+$"))
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
           
            //if (Schema.operation.Contains('\''))
            //{

            //}
        }
    }
}
