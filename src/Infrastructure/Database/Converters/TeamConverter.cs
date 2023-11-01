using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Converters;

public static class TeamConverter
{
    public static Domain.Entities.Team ToDomainEntity(Team team) => new()
    {
        Id = team.Id,
        Name = team.Name
    };
}