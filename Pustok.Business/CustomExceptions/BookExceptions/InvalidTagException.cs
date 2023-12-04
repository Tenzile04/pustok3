namespace Pustokk.CustomExceptions.BookExceptions
{
    public class InvalidTagException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidTagException()
        {

        }
        public InvalidTagException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }
    }
}
