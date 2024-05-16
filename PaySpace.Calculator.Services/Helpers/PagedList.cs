using Microsoft.EntityFrameworkCore;
using MapsterMapper;

namespace PaySpace.Calculator.Services.Helpers;

public class PagedList<T>
{
    private PagedList(List<T> items, int page, int pageSize, int total)
    {
        Data = items;
        Page = page;
        PageSize = pageSize;
        Total = total;
    }

    public List<T> Data { get; }
    public int Total { get; }
    public int Page { get; }
    public int PageSize { get; }

    /// <summary>
    /// Returns a paged list of the query <see cref="IQueryable"/>
    /// </summary>
    /// <typeparam name="TDest">Destination type</typeparam>
    /// <param name="mapper">Mapper with registered type mappings to use</param>
    /// <param name="query">Query of the entity</param>
    /// <param name="request"><see cref="QueryRequest"/></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Paged list of the provided destination type</returns>
    public static async Task<PagedList<TDest>> CreateAsync<TDest>(IMapper mapper, IQueryable<T> query, QueryRequest request, CancellationToken cancellationToken)
    {
        var totalCount = await query.CountAsync(cancellationToken: cancellationToken);
        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => mapper.Map<T, TDest>(x))
            .ToListAsync(cancellationToken);

        return new(items, request.Page, request.PageSize, totalCount);
    }

    /// <summary>
    /// Converts a lis into a paged list format
    /// </summary>
    /// <typeparam name="TDest">Destination type</typeparam>
    /// <param name="mapper">Mapper with registered type mappings to use</param>
    /// <param name="list">Original list to convert</param>
    /// <returns>Paged list of the provided destination type</returns>
    public static PagedList<TDest> Create<TDest>(IMapper mapper, List<T> list)
    {
        var totalCount = list.Count;
        var items = list
            .Select(x => mapper.Map<T, TDest>(x))
            .ToList();

        return new(items, 1, totalCount, totalCount);
    }
}
