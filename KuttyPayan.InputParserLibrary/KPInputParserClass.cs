using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KuttyPayan.DBReaderLibrary;
using KuttyPayan.MongodbLibrary;
using KuttyPayan.NLP;
using KuttyPayan.Entities;

namespace KuttyPayan.InputParserLibrary
{
    public class KPInputParserClass
    {
        public string KPInputParserMethod(string SearchInput)
        {
            StringBuilder output = new StringBuilder();
            KuttyPayanPosTaggerClass objNLP = new KuttyPayanPosTaggerClass();
            string[] sentences = objNLP.SplitSentences(SearchInput);

            foreach (string sentence in sentences)
            {
                string[] tokens = objNLP.TokenizeSentence(sentence);
                string[] tags = objNLP.PosTagTokens(tokens);

                output.Append(objNLP.ChunkSentence(tokens, tags)).Append("\r\n");
            }
            InputParserEntities WordsEntitiesList = KPInputDelimiter(output.ToString());

            KuttyPayan.MongodbLibrary.KuttyPayanMongodbClass objKPDB = new KuttyPayanMongodbClass();
            bool isInserted = objKPDB.KuttyPayanInputParserInsert(WordsEntitiesList);

            return output.ToString();
        }
        private InputParserEntities KPInputDelimiter(string ParsedInput)
        {
            InputParserEntities Entities = new InputParserEntities();


            string[] InputString = ParsedInput.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            List<WordsEntities> WordGroup = new List<WordsEntities>();
            WordsEntities Word = null;
            foreach (string words in InputString)
            {

                if (!string.IsNullOrEmpty(words.Trim()))
                {
                    Word = new WordsEntities();
                    string[] splittedWords = words.Trim().Split(' ');
                    if (splittedWords.Count() > 1)
                    {
                        List<WordTagPair> WordtagPairList = new List<WordTagPair>();
                        Word.ChunkerGroup = splittedWords[0];
                        for (int count = 1; count < splittedWords.Count(); count++)
                        {
                            WordTagPair WordTag = new WordTagPair();

                            string[] WordtagPair = splittedWords[count].Split('/');
                            WordTag.tag = WordtagPair[1];
                            WordTag.Word = WordtagPair[0];
                            WordtagPairList.Add(WordTag);
                        }
                        Word.Words = WordtagPairList;

                    }
                    if (Word.ChunkerGroup != null && Word.Words != null)
                    {
                        WordGroup.Add(Word);
                    }
                }
            }
            Entities.WordGroup = WordGroup;
            return Entities;
        }
    }
}