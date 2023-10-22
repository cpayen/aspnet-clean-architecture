using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllTeamsQuery : IRequest<IEnumerable<Team>> {}

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IEnumerable<Team>>
{
    private readonly ITeamRepository _teamRepository;

    public GetAllTeamsQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<IEnumerable<Team>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => _teamRepository.GetAllAsync(), cancellationToken);
    }
}