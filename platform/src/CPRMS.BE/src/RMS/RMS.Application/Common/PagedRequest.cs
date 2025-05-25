namespace Rms.Application.Common
{
    public enum FilterOperator
    {
        Equals,
        NotEquals,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith,
        In
    }

    public class FilterCriterion
    {
        public required string FieldName { get; init; }
        public FilterOperator Operator { get; init; }
        public object? Value { get; init; }
    }

    public class SortDescriptor
    {
        public required string FieldName { get; init; }
        public bool IsDescending { get; init; } = false;
    }

    public class PagedRequest<TItem, TAdvancedFilter>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? SearchTerm { get; init; }
        public List<FilterCriterion>? Filters { get; init; }
        public List<SortDescriptor>? SortOrders { get; init; }
        public bool IsAdvancedSearch { get; init; } = false;
        public TAdvancedFilter? AdvancedFilterModel { get; init; }
    }
}
