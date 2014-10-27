using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuttyPayan.SchemaEvaluatorLibrary
{
    public class SchemaEntityClass
    {
        public string SchemaName { get; set; }
        public int WordCount { get; set; }
        public List<WordSchemaReferenceValueClass> WordSchemaReferenceValueCollection { get; set; }
        public string InputSearch { get; set; }
        public int WordSchemaMatchCount { get; set; }
        public DateTime SearchDateTime { get; set; }
    }
    public class WordSchemaReferenceValueClass
    {
        public string Word { get; set; }
        public string SchemaReference { get; set; }
        public string SchemaValue { get; set; }
        
    }

}
