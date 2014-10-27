using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KuttyPayan.DBReaderLibrary
{

    public class KuttyPayanDbReaderClass
    {
        public List<Dictionary> FileReadMethod()
        {
            List<Dictionary> DictionaryList = new List<Dictionary>();
            string[] lines = System.IO.File.ReadAllLines(@"G:\VS2013 Projects\ASP MVC\KuttyPayan\KuttyPayan.DBReaderLibrary\wn_database.txt");
            foreach (string line in lines)
            {
                //string[] Words = line.Split('|');
                //string[] wordList = `[0].Split(',');
                //string PartOfSpeech = Words[1];
                //string[] MeaningList = Words[2].Split(';');
                //string[] UsageList = Words[3].Split(';');

                //if (wordList.Count() > 1)
                //{
                //    string JsonFormat = "{word : " + wor + "}";
                //}


                Dictionary objDic = new Dictionary();

                //string InputData = "absent,remove|v|go away or leave ; Go away |He absented himself";
                string InputData = line;
                string[] Words = InputData.Split('|');
                string DicWords = Words[0];
                string[] WordList = DicWords.Trim().Split(',');
                string DicMeanings = Words[2];
                string[] Meanings = DicMeanings.Trim().Split(';');
                string DicUsage = Words[3];
                string[] Usages = DicUsage.Trim().Split(';');

                List<string> RelatedWordsList = new List<string>();
                if (WordList.Length > 1)
                {
                    for (int i = 0; i < WordList.Length; i++)
                    {
                        if (i == 0)
                        {
                            objDic.Word = WordList[i];
                        }
                        else if (i > 0)
                        {
                            RelatedWordsList.Add(WordList[i]);
                        }
                    }
                }
                else
                {
                    objDic.Word = WordList[0];
                }
                objDic.Meanings = Meanings;
                objDic.RelatedWords = RelatedWordsList.ToArray();
                objDic.partOfSpeech = Words[1];
                objDic.Usages = Usages;

                //absent,remove|v|go away or leave|"He absented himself"

                //objDic.Word = "absent";
                //objDic.RelatedWords = new string[] { "remove" };            
                //objDic.partOfSpeech = "v";
                //objDic.Meanings = new string[] { "go away or leave","to go away" };
                //objDic.Usages = new string[] { "He absented himself" };              
                ////string json = JsonConvert.SerializeObject(objDic);
                ////Console.WriteLine(json);
                DictionaryList.Add(objDic);
            }
            return DictionaryList;
        }

        public List<POSTags> TagReadMethod()
        {
            List<POSTags> POSTagList = new List<POSTags>();
            string[] lines = System.IO.File.ReadAllLines(@"G:\VS2013 Projects\ASP MVC\KuttyPayan\KuttyPayan.DBReaderLibrary\DBInput\POSTags.txt");
            foreach (string line in lines)
            {
                POSTags objPosTags = new POSTags();
                string InputData = line;
                string[] Words = InputData.Split(':');
                string TagName = Words[0].Trim();
                string TagDescription = Words[1].Trim();
                objPosTags.TagName = TagName;
                objPosTags.TagDescription = TagDescription;
                POSTagList.Add(objPosTags);
            }
            return POSTagList;
        }
        public List<SearchInputTags> SearchTextToDBMethod(Dictionary<string, string> POSDictionay, string InputString)
        {
            List<SearchInputTags> SearchInput = new List<SearchInputTags>();
            SearchInputTags objSearchTags = new SearchInputTags();
            objSearchTags.SearchText = InputString;
            foreach (KeyValuePair<string, string> Entity in POSDictionay)
            {
                objSearchTags.POSTag = Entity.Value;
                objSearchTags.SearchToken = Entity.Key;
            }
            SearchInput.Add(objSearchTags);
            return SearchInput;
        }

        public List<EmployeeSample> InsertEmployees()
        {
            List<EmployeeSample> EmpoyeeList = new List<EmployeeSample>();
            string[] lines = System.IO.File.ReadAllLines(@"G:\VS2013 Projects\ASP MVC\KuttyPayan\KuttyPayan.DBReaderLibrary\DBInput\EmployeeInput.txt");
            foreach (string line in lines)
            {
                EmployeeSample objEmpoyee = new EmployeeSample();
                string InputData = line;
                string[] Words = InputData.Split('\t');
                int EmpID = Convert.ToInt32(Words[0].Trim());
                string EmpName = Words[1].Trim();
                string EmpDesig = Words[2].Trim();
                string EmpAddr = Words[3].Trim();
                objEmpoyee.EmpId = EmpID;
                objEmpoyee.EmpName = EmpName;
                objEmpoyee.EmpDesig = EmpDesig;
                objEmpoyee.EmpAddress = EmpAddr;
                EmpoyeeList.Add(objEmpoyee);
            }
            return EmpoyeeList;

        }
        public List<EmployeeSample> InsertEmployeeSchema(EmployeeSchema EmpSchema)
        {
            List<EmployeeSample> EmpoyeeList = new List<EmployeeSample>();
            string[] lines = System.IO.File.ReadAllLines(@"G:\VS2013 Projects\ASP MVC\KuttyPayan\KuttyPayan.DBReaderLibrary\DBInput\EmployeeInput.txt");
            foreach (string line in lines)
            {
                EmployeeSample objEmpoyee = new EmployeeSample();
                string InputData = line;
                string[] Words = InputData.Split('\t');
                int EmpID = Convert.ToInt32(Words[0].Trim());
                string EmpName = Words[1].Trim();
                string EmpDesig = Words[2].Trim();
                string EmpAddr = Words[3].Trim();
                objEmpoyee.EmpId = EmpID;
                objEmpoyee.EmpName = EmpName;
                objEmpoyee.EmpDesig = EmpDesig;
                objEmpoyee.EmpAddress = EmpAddr;
                EmpoyeeList.Add(objEmpoyee);
            }
            return EmpoyeeList;

        }
    }
}
