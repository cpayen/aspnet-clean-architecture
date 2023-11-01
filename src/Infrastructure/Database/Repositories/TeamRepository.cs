using Application.Contracts;
using Infrastructure.Database.Converters;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly Context _context;
    
    public TeamRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Domain.Entities.Team>> GetAllAsync()
    {
        var result = await _context.Teams.ToListAsync();
        return result.Select(TeamConverter.ToDomainEntity);
    }
    
    public async Task<Domain.Entities.Team?> GetAsync(Guid id)
    {
        var result = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? null
            : TeamConverter.ToDomainEntity(result);
    }
    
    public async Task<Guid> CreateAsync(Domain.Entities.Team team)
    {
        var entity = new Team
        {
            Name = team.Name,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now
        };

        await _context.Teams.AddAsync(entity);
        return entity.Id;
    }
    
    public async Task UpdateAsync(Domain.Entities.Team team)
    {
        var entity = await _context.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);

        if (entity == null)
        {
            throw new ArgumentException($"Team with ID {team.Id} not found. Update failed.");
        }

        entity.Name = team.Name;
        entity.UpdatedAt = DateTimeOffset.Now;

        _context.Teams.Update(entity);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
        
        if (entity == null)
        {
            throw new ArgumentException($"Team with ID {id} not found. Delete failed.");
        }
        
        _context.Teams.Remove(entity);
    }
}