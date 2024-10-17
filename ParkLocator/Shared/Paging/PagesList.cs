using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ParkLocator.Shared.Paging;

public class PagedList<T>
{
    private PagedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }
    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public bool HasNextPages => Page * PageSize < TotalCount;
    public bool HasPreviousPages => Page > 1;
    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> queryItems, int page, int pageSize, CancellationToken cancellation)
    {
        int totalCount = await queryItems.CountAsync(cancellation);
        List<T> items = await queryItems.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellation);
        return new(items, page, pageSize, totalCount);
    }
}
