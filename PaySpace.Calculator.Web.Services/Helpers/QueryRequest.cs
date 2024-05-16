using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Web.Services.Helpers
{
    public class QueryRequest
    {
        [Required]
        [DefaultValue(1)]
        public int Page { get; set; } = 1;
        [Required]
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;

        public string? SortColumn { get; set; } = string.Empty;

        public string? SortOrder { get; set; } = string.Empty;

        public override string ToString() =>
            $"page={Page}&pageSize={PageSize}&sortColumn={SortColumn}&sortOrder={SortOrder}";
    }
}
