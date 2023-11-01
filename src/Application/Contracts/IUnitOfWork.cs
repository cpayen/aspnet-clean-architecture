namespace Application.Contracts;

public interface IUnitOfWork
{
    ITeamRepository TeamRepository { get; }
    IPlayerRepository PlayerRepository { get; }

    Task SaveAsync();
}