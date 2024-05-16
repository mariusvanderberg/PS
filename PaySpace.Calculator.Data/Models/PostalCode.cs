using System.ComponentModel.DataAnnotations;

namespace PaySpace.Calculator.Data.Models;

public sealed class PostalCode
{
    [Key]
    public long Id { get; set; }

    public string Code { get; set; } = string.Empty!;

    public CalculatorType Calculator { get; set; }
}