using PaySpace.Calculator.Services.Common;

namespace PaySpace.Calculator.Services.Exceptions;

public sealed class SettingsException : ArgumentNullException
{
    public SettingsException() : base(Constants.NO_SETTINGS_FOUND) { }
    public SettingsException(string message) : base(message) { }
    public SettingsException(string message, Exception inner) : base(message, inner) { }
}