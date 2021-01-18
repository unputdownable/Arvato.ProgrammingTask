namespace Common.Fixer.Models
{
    public class ErrorResponse : Response
    {
        public Error Error { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Info { get; set; }
    }
}
