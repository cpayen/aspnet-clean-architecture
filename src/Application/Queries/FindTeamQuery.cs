using Application.Contracts;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class FindTeamQuery : IRequest<Team>
{
    internal Guid Id { get; }

    public FindTeamQuery(Guid id)
    {
        Id = id;
    }
}

public class FindTeamQueryHandler : IRequestHandler<FindTeamQuery, Team>
{
    private readonly ITeamRepository _teamRepository;

    public FindTeamQueryHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<Team> Handle(FindTeamQuery request, CancellationToken cancellationToken)
    {
        var team = await _teamRepository.FindAsync(request.Id) ?? 
                   throw new NotFoundException("Oups, not found");
        return team;
    }
}