using PaySpace.Calculator.Services.Common;

namespace PaySpace.Calculator.Services.Exceptions;

public sealed class CalculatorException : InvalidOperationException
{
    public CalculatorException() : base(Constants.INVALID_POSTAL_CODE) { }
    public CalculatorException(string message) : base(message) { }
    public CalculatorException(string message, Exception inner) : base(message, inner) { }    
}