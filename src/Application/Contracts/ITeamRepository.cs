using Domain.Entities;

namespace Application.Contracts;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team?> FindAsync(Guid id);
    Team Create(Team team);
    Team Update(Team team);
    Team Delete(Guid id);
}