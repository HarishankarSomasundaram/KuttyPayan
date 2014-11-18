using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuttyPayan.NLP
{
    public class KuttyPayanPosTaggerClass
    {
        private string mModelPath =  @"G:\Works\NLP\NLP Library\";
        
        private OpenNLP.Tools.SentenceDetect.MaximumEntropySentenceDetector mSentenceDetector;
        private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        private OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;
        private OpenNLP.Tools.Chunker.EnglishTreebankChunker mChunker;
        private OpenNLP.Tools.Parser.EnglishTreebankParser mParser;
        private OpenNLP.Tools.NameFind.EnglishNameFinder mNameFinder;
        private OpenNLP.Tools.Lang.English.TreebankLinker mCoreferenceFinder;
        public Dictionary<string, string> PosTaggerMethod(string InputString)
        {
            StringBuilder output = new StringBuilder();

            string[] sentences = SplitSentences(InputString);
            Dictionary<string, string> TokenTags = new Dictionary<string, string>();

            foreach (string sentence in sentences)
            {
                string[] tokens = TokenizeSentence(sentence);
                string[] tags = PosTagTokens(tokens);
                 
                for (int currentTag = 0; currentTag < tags.Length; currentTag++)
                {
                    TokenTags.Add(tokens[currentTag], tags[currentTag]);
                   // output.Append(tokens[currentTag]).Append("/").Append(tags[currentTag]).Append(" ");
                }

                //output.Append("\r\n\r\n");
            }

            return TokenTags;
        }
        public string[] SplitSentences(string paragraph)
        {
            if (mSentenceDetector == null)
            {
                mSentenceDetector = new OpenNLP.Tools.SentenceDetect.EnglishMaximumEntropySentenceDetector(mModelPath + "EnglishSD.nbin");
            }

            return mSentenceDetector.SentenceDetect(paragraph);
        }
        public string[] TokenizeSentence(string sentence)
        {
            if (mTokenizer == null)
            {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }
        public string[] PosTagTokens(string[] tokens)
        {
            if (mPosTagger == null)
            {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(mModelPath + "EnglishPOS.nbin", mModelPath + @"\Parser\tagdict");
            }

            return mPosTagger.Tag(tokens);
        }
        public string ChunkSentence(string[] tokens, string[] tags)
        {
            if (mChunker == null)
            {
                mChunker = new OpenNLP.Tools.Chunker.EnglishTreebankChunker(mModelPath + "EnglishChunk.nbin");
            }

            return mChunker.GetChunks(tokens, tags);
        }
    }
}
