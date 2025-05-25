using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models.Base
{
    public class QueryResult 
    {
        public EntityResult Result { get; set; } = default!;
        public PagingInfo Paging { get; set; } = default!;
    }
    public class EntityResult
    {
        public string PrimaryEntityName { get; set; } = String.Empty;
        public long TotalItemsCount { get; set; }
        public List<Dictionary<string, FieldResult>> Records { get; set; } = new List<Dictionary<string, FieldResult>>();
    }
    public class PagingInfo
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool ReturnTotalItemsCount { get; set; } = false;
    }
    public class FieldResult
    {
        public string FieldName { get; set; } = String.Empty;
        public MetaFieldType Type { get; set; }
        public object Value { get; set; } = default!;
    }
    public enum MetaFieldType
    {
        None = 0,
        Int = 1,
        Long = 2,
        String = 3,
        Bool = 7,
        Double = 9,
        Guid = 10,
        Date = 11,
        DateTime = 12,
        Lookup = 15,
        OptionSet = 16,
        Memo = 17,
        Mutiple = 19,
        Document = 20,
        Currency = 21,
        Image = 99
    }
}
