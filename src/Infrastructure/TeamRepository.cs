using Application.Contracts;
using Domain.Entities;

namespace Infrastructure;

public class TeamRepository : ITeamRepository
{
    private static readonly List<Team> Teams = new();
    
    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await Task.Run(() => Teams);
    }

    public async Task<Team?> FindAsync(Guid id)
    {
        return await Task.Run(() => Teams.SingleOrDefault(x => x.Id == id));
    }

    public Team Create(Team team)
    {
        Teams.Add(team);
        return team;
    }

    public Team Update(Team team)
    {
        Teams.Single(x => x.Id == team.Id).Name = team.Name;
        return team;
    }

    public Team Delete(Guid id)
    {
        var team = Teams.Single(x => x.Id == id);
        Teams.Remove(team);
        return team;
    }
}