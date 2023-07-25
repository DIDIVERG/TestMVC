namespace WebApplication10.Exceptions;

public class EntityNotExistException : ApplicationException
{
    
    public EntityNotExistException(string message) : base(message)
    {
        
    }

    public EntityNotExistException(string message, Exception innerException) : base(message,innerException)
    {
        
    }
}