namespace Application.Exceptions;

public class UnavailablePlayerNumberException : BadRequestException
{
    public UnavailablePlayerNumberException(string message) : base(message) {}
    
    public UnavailablePlayerNumberException(int number, string teamName)
        : this($"Player with number {number} already exist for the team {teamName}.") {}
}