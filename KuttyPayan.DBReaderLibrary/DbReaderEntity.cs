using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace KuttyPayan.DBReaderLibrary
{
    //public class DbReaderEntity
    //{
    //    public ObjectId Id { get; set; }
    //    public string Word { get; set; }
    //    public string[] ReletedWords { get; set; }
    //    public string[] Meaning { get; set; }
    //    public string[] Usage { get; set; }
    //}
    public class CRUDSchema
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
        public string MappedSchema { get; set; }
        public List<string[]> column { get; set; }
    }
    public class Dictionary
    {
        public ObjectId Id { get; set; }
        public string Word { get; set; }
        public string[] RelatedWords { get; set; }
        public string partOfSpeech { get; set; }
        public string[] Meanings { get; set; }
        public string[] Usages { get; set; }
    }
    public class POSTags
    {
        public ObjectId Id { get; set; }
        public string TagName { get; set; }
        public string TagDescription { get; set; }
    }
    public class SearchInputTags
    {
        public ObjectId Id { get; set; }
        public string SearchText { get; set; }
        public string SearchToken { get; set; }
        public string POSTag { get; set; }
    }
    public class EmployeeSample
    {
        public ObjectId Id { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string EmpDesig { get; set; }
        public string EmpAddress { get; set; }
    }
    public class EmployeeSchema
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string Type { get; set; }
        public List<string[]> Data { get; set; }
        public List<string[]> ReferenceTable { get; set; }
    }
    public class SchemaMap
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Length { get; set; }
        public string group { get; set; }
        public string MappedSchema { get; set; }
        public string[] column { get; set; }
        public string[] operation { get; set; }
    }
    public class EmployeeReference
    {
        public ObjectId Id { get; set; }
        public string Key { get; set; }
        public string Reference { get; set; }
    }

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
