using Application.Contracts;
using Infrastructure.Database.Converters;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly Context _context;
    
    public PlayerRepository(Context context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Domain.Entities.Player>> GetAllAsync()
    {
        var result = await _context.Players.Include(x => x.Team).ToListAsync();
        return result.Select(PlayerConverter.ToDomainEntity);
    }

    public async Task<IEnumerable<Domain.Entities.Player>> FindByTeamAsync(Guid id)
    {
        var result = await _context.Players.Include(x => x.Team).Where(x => x.Team!.Id == id).ToListAsync();
        return result.Select(PlayerConverter.ToDomainEntity);
    }

    public async Task<Domain.Entities.Player?> GetAsync(Guid id)
    {
        var result = await _context.Players.Include(x => x.Team).FirstOrDefaultAsync(x => x.Id == id);
        return result == null
            ? null
            : PlayerConverter.ToDomainEntity(result);
    }

    public async Task<Guid> CreateAsync(Domain.Entities.Player player)
    {
        var team = 
            await _context.Teams.FirstOrDefaultAsync(x => x.Id == player.Team!.Id)
            ?? throw new ArgumentException($"Team with ID {player.Team!.Id} does not exist. Create failed.");

        var entity = new Player
        {
            Name = player.Name,
            Number = player.Number,
            Team = team,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now
        };

        await _context.Players.AddAsync(entity);
        return entity.Id;
    }

    public async Task UpdateAsync(Domain.Entities.Player player)
    {
        var entity = 
            await _context.Players.FirstOrDefaultAsync(x => x.Id == player.Id)
            ?? throw new ArgumentException($"Player with ID {player.Id} not found. Update failed.");
        
        var team = 
            await _context.Teams.FirstOrDefaultAsync(x => x.Id == player.Team!.Id)
            ?? throw new ArgumentException($"Team with ID {player.Team!.Id} does not exist. Update failed.");

        entity.Name = player.Name;
        entity.Team = team;
        entity.Number = player.Number;
        entity.UpdatedAt = DateTimeOffset.Now;

        _context.Players.Update(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = 
            await _context.Players.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new ArgumentException($"Player with ID {id} not found. Delete failed.");
        
        _context.Players.Remove(entity);
    }
}