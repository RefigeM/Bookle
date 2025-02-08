namespace Bookle.BL.Exceptions;

public class NotFoundException: Exception
{
	public string ErrorMessage { get; }
	public NotFoundException() : base("Not found.")
	{
		ErrorMessage = "Not found.";
	}
	public NotFoundException(string message) : base(message)
	{
		ErrorMessage = message;
	}
}
