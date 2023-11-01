using Application.Contracts;

namespace Application.Services;

public class PlayerService
{
    private readonly IUnitOfWork _uow;
    
    public PlayerService(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<bool> CheckNumberIsAvailableForTeam(int number, Guid teamId)
    {
        var players = await _uow.PlayerRepository.FindByTeamAsync(teamId);
        return players.All(x => x.Number != number);
    }
}