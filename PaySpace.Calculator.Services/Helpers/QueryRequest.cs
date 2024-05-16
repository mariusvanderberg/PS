using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Services.Helpers;

public class QueryRequest
{
    [Required]
    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    [Required]
    [DefaultValue(20)]
    public int PageSize { get; set; } = 20;

    public string? SortColumn { get; set; } = string.Empty;

    public string? SortOrder { get; set; } = string.Empty;
}
