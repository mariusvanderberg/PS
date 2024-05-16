namespace PaySpace.Calculator.Tests.Common;

public static class Constants
{
    public static class Format
    {
        private static readonly string RESOLVE_ERROR = "Cannot resolve {0}";
        public static string FormatResolveError(string name) => string.Format(RESOLVE_ERROR, name);
    }
}
