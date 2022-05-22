namespace SC.SenseTower.Common.Exceptions
{
    public class ScException : Exception
    {
        public ScException(string? message) : base(message) { }

        public ScException(Exception? exception, string? message) : base(message, exception) { }
    }
}
