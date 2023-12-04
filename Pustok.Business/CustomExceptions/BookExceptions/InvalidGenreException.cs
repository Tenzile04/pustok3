namespace Pustokk.CustomExceptions.BookExceptions
{
    public class InvalidGenreException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidGenreException()
        {
            
        }
        public InvalidGenreException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
