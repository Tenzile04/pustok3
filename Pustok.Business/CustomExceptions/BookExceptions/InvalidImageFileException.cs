﻿namespace Pustokk.CustomExceptions.BookExceptions
{
    public class InvalidImageFileException:Exception
    {
        public string PropertyName { get; set; }
        public InvalidImageFileException()
        {
            
        }
        public InvalidImageFileException(string propertyName, string message) : base(message)
        {
            PropertyName = propertyName;

        }
    }
}
