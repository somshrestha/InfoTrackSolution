namespace InfoTract.Helpers.ExceptionHandler
{
    public class SearchException : Exception
    {
        public SearchException() { }
        public SearchException(string message) : base(message) { }
        public SearchException(string message, Exception ex) : base(message, ex) { }
    }
}
