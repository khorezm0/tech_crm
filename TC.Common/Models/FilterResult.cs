namespace TC.Common.Models;

public class FilterResult<T>
{
    public long Offset { get; set; }
    public long Count { get; set; }
    public long TotalCount { get; set; }
    public IEnumerable<T> Data { get; set; }

    public FilterResult()
    {
        Data = Array.Empty<T>();
    }
    
    public FilterResult(IEnumerable<T> data, long totalCount, long offset, long count)
    {
        Data = data;
        Offset = offset;
        Count = count;
        TotalCount = totalCount;
    }
}