using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaySpace.Calculator.Web.Models
{
    public sealed class CalculatorViewModel
    {
        [Display(Name = "Postal Codes")]
        public SelectList PostalCodes { get; set; } = null!;

        [Display(Name = "Postal Code")]
        [StringLength(5, MinimumLength = 3)]
        public string PostalCode { get; set; } = string.Empty!;

        [Display(Name = "Income")]
        [DataType(DataType.Currency)]
        public decimal Income { get; set; }
    }
}