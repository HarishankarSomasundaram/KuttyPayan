using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using KuttyPayan.DBReaderLibrary;
using KuttyPayan.MongodbLibrary;
using System.ComponentModel.DataAnnotations;
namespace Kuttypayan.Web.Models
{
    public class DictionaryModel
    {
        [DisplayName("Search Dictionary")]
        [Required(ErrorMessage = "Please type something")]
        public string Search { get; set; }
        public List<Dictionary> SearchResult { get; set; }
        public string NLPSearchResult { get; set; }
    }
    public class SearchLogic
    {
        public List<Dictionary> SearchModel(string DictionaryKey)
        {
            KuttyPayanMongodbClass KPdbObj = new KuttyPayanMongodbClass();
            return KPdbObj.KuttyPayanSearchMethod(DictionaryKey);

        }
    }
}