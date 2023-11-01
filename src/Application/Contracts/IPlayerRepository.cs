using System.Linq.Expressions;
using Domain.Entities;

namespace Application.Contracts;

public interface IPlayerRepository
{
    Task<IEnumerable<Player>> GetAllAsync();
    Task<IEnumerable<Player>> FindByTeamAsync(Guid teamId);
    Task<Player?> GetAsync(Guid id);
    Task<Guid> CreateAsync(Player player);
    Task UpdateAsync(Player player);
    Task DeleteAsync(Guid id);
}