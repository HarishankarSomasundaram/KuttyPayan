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

            WordsEntitiesList = KPInputNoiceFilter(WordsEntitiesList);
            KuttyPayan.MongodbLibrary.KuttyPayanMongodbClass objKPDB = new KuttyPayanMongodbClass();
            bool isInserted = objKPDB.KuttyPayanInputParserInsert(WordsEntitiesList);
            //output.Append(WordsEntitiesList.WordGroup.SelectMany(x => x.Words).ToString());
            return output.ToString();
        }

        private InputParserEntities KPInputNoiceFilter(InputParserEntities WordsEntitiesList)
        {
            InputParserEntities Entries = (InputParserEntities) WordsEntitiesList.
            obj myobj2 = (obj)myobj.MemberwiseClone();
            List<WordsEntities> WordsEntities = new  WordsEntitiesList.WordGroup;
            WordsEntities = WordsEntitiesList.WordGroup;
            List<WordTagPair> Words = new List<WordTagPair>();

            for (int i = 0; i < WordsEntitiesList.WordGroup.Count; i++)
            {
                for (int j = 0; j < WordsEntitiesList.WordGroup[i].Words.Count; j++)
                {

                    if (WordsEntitiesList.WordGroup[i].Words[j].tag == "PRP")
                    {
                        WordsEntities.Remove(WordsEntities[i]);
                    }
                    if (WordsEntitiesList.WordGroup[i].Words[j].Word.ToLower() == "please")
                    {
                        WordTagPair wtp = WordsEntitiesList.WordGroup[i].Words[j];
                        WordsEntities[i].Words.Remove(wtp);
                    }

                    if (WordsEntitiesList.WordGroup[i].Words.Count == 0)
                    {
                        WordsEntities we = WordsEntitiesList.WordGroup[i];
                        WordsEntitiesList.WordGroup.Remove(we);

                    }
                }
            }
            Entries.WordGroup = WordsEntities;
            return Entries;
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