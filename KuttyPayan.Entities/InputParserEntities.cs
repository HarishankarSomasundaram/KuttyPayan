using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuttyPayan.Entities
{
    public class InputParserEntities
    {
        public List<WordsEntities> WordGroup { get; set; }
    }
    public class WordsEntities
    {
        public string ChunkerGroup { get; set; }
        public List<WordTagPair> Words { get; set; }
    }
    public class WordTagPair
    {
        public string tag { get; set; }
        public string Word { get; set; }
    }
}
