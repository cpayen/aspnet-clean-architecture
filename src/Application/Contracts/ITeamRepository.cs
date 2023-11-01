using Domain.Entities;

namespace Application.Contracts;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team?> GetAsync(Guid id);
    Task<Guid> CreateAsync(Team team);
    Task UpdateAsync(Team team);
    Task DeleteAsync(Guid id);
}