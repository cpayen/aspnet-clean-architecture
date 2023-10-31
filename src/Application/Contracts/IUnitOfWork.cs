namespace Application.Contracts;

public interface IUnitOfWork
{
    ITeamRepository TeamRepository { get; }

    Task SaveAsync();
}