using Application.Contracts;
using Infrastructure.Database.Entities;
using Infrastructure.Database.Entities.Abstractions;
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
        return result.Select(x => new Domain.Entities.Team()
        {
            Id = x.Id,
            Name = x.Name
        });
    }
    
    public async Task<Domain.Entities.Team?> GetAsync(Guid id)
    {
        var result = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? null
            : new Domain.Entities.Team()
            {
                Id = result.Id,
                Name = result.Name
            };
    }
    
    public async Task<Domain.Entities.Team> CreateAsync(Domain.Entities.Team team)
    {
        var entity = new Team
        {
            Name = team.Name,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now
        };

        await _context.Teams.AddAsync(entity);

        team.Id = entity.Id;
        return team;
    }
    
    public async Task<Domain.Entities.Team> UpdateAsync(Domain.Entities.Team team)
    {
        var entity = await _context.Teams.FirstOrDefaultAsync(x => x.Id == team.Id);

        if (entity == null)
        {
            throw new ArgumentException($"Team with ID {team.Id} not found. Update failed.");
        }

        entity.Name = team.Name;
        entity.UpdatedAt = DateTimeOffset.Now;

        _context.Teams.Update(entity);
        return team;
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