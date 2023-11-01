namespace WebApp.Dto;

public record PlayerDto(Guid Id, string Name, int Number, Guid TeamId, string TeamName)
{
    public static PlayerDto FromDomainEntity(Domain.Entities.Player player)
        => new PlayerDto(player.Id, player.Name, player.Number, player.Team!.Id, player.Team.Name);
}
public record CreatePlayerDto(string Name, Guid TeamId, int Number);
public record UpdatePlayerDto(string Name, Guid TeamId, int Number);