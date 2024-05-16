using PaySpace.Calculator.Web.Services.Helpers;
using PaySpace.Calculator.Web.Services.Models;

namespace PaySpace.Calculator.Web.Models
{
    public sealed class CalculatorHistoryViewModel
    {
        public PagedList<CalculatorHistory>? CalculatorHistory { get; set; }
    }
}