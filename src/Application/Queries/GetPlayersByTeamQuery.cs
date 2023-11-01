using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetPlayersByTeamQuery : IRequest<IEnumerable<Player>>
{
    internal Guid TeamId { get; }

    public GetPlayersByTeamQuery(Guid teamId)
    {
        TeamId = teamId;
    }
}

public class FindPlayersByTeamQueryHandler : IRequestHandler<GetPlayersByTeamQuery, IEnumerable<Player>>
{
    private readonly IUnitOfWork _uow;

    public FindPlayersByTeamQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<Player>> Handle(GetPlayersByTeamQuery request, CancellationToken cancellationToken)
    {
        return await _uow.PlayerRepository.FindByTeamAsync(request.TeamId);
    }
}