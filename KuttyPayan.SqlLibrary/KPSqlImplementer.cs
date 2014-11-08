using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuttyPayan.SqlLibrary
{
    public class KPSqlImplementer
    {
        public string SqlImplementerMethod(string ParsedString)
        {
            string result = string.Empty;
            SqlConnection conn = new SqlConnection(
           "Data Source=HARI;Initial Catalog=Haridb;Integrated Security=SSPI");
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(ParsedString, conn);
                rdr = cmd.ExecuteReader();
                List<Dictionary<string, object>> DictList = new List<Dictionary<string, object>>();
                while (rdr.Read())
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for (int lp = 0; lp < rdr.FieldCount; lp++)
                    {
                        dict.Add(rdr.GetName(lp), rdr.GetValue(lp));
                    }
                    DictList.Add(dict);
                }
              result=  HtmlFormater(DictList);
            }
            catch (SqlException e)
            {

            }
            finally
            {

                if (rdr != null)
                {
                    rdr.Close();
                }
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return result;
        }
        public string HtmlFormater(List<Dictionary<string, object>> DictList)
        {
            string result = string.Empty;
            if(DictList.Count>0)
            {
                string[] TableHeader = DictList.Select(a => a.Keys).FirstOrDefault().ToArray();
                
                StringBuilder HtmlSb = new StringBuilder();
                HtmlSb.Append("<table border='1'>");
        
                HtmlSb.Append("<tr>");
                foreach (string header in TableHeader)
                {
                    HtmlSb.Append("<th>");
                    HtmlSb.Append(header);
                    HtmlSb.Append("</th>");
                }
                HtmlSb.Append("</tr>");
            
                foreach(Dictionary<string,object> Table in DictList)
                {
                    HtmlSb.Append("<tr>");
                    foreach(KeyValuePair<string,object> column in Table)
                    {
                        HtmlSb.Append("<td>");
                        HtmlSb.Append(column.Value);
                        HtmlSb.Append("</td>");
                    }
                    HtmlSb.Append("</tr>"); 
                }
                HtmlSb.Append("</table>");
                result = HtmlSb.ToString();
            }
            return result;
        }

    }
}
