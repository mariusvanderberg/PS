namespace PaySpace.Calculator.Web.Services.Helpers;

public class PagedList<T>
{
    public List<T> Data { get; set; } = null!;
    public int Total { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
