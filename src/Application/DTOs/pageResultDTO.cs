using System.Collections.Generic;

namespace Application.DTOs;

public class PagedResult<T>
{
    public IReadOnlyList<T> Items { get; } = Array.Empty<T>(); // Interface có sẵn
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalPages => (TotalCount + PageSize - 1) / PageSize;
    
     public PagedResult(IReadOnlyList<T> items, int totalCount, int page, int pageSize)
    {
        Items = items ?? Array.Empty<T>();
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }
}