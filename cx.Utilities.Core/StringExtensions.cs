namespace cx.Utilities.Core
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string source, string destination)
        {
            if (source == null || destination == null) return false;
            return source.Equals(destination, StringComparison.OrdinalIgnoreCase);
        }
        public static bool ContainsIgnoreCase(this string source, string destination)
        {
            if (source == null || destination == null
                || string.IsNullOrWhiteSpace(source)
                || string.IsNullOrWhiteSpace(destination)) return false;
            return source.ToLower().Contains(destination.ToLower());
        }
        public static string ToExpandEnvironmentVariable(this string variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName)) return "";
            return Environment.ExpandEnvironmentVariables(variableName);
        }
    }
}