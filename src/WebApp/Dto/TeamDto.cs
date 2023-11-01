namespace WebApp.Dto;

public record TeamDto(Guid Id, string Name)
{
    public static TeamDto FromDomainEntity(Domain.Entities.Team entity) 
        => new TeamDto(entity.Id, entity.Name);
}
public record CreateTeamDto(string Name);
public record UpdateTeamDto(string Name);
