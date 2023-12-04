namespace Pustokk.CustomExceptions.BookExceptions
{
    public class InvalidAuthorException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidAuthorException()
        {

        }
        public InvalidAuthorException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
