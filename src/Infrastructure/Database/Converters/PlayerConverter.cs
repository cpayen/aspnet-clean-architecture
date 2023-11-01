using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Converters;

public static class PlayerConverter
{
    public static Domain.Entities.Player ToDomainEntity(Player player) => new ()
    {
        Id = player.Id,
        Name = player.Name,
        Number = player.Number,
        Team = TeamConverter.ToDomainEntity(player.Team!)
    };
}