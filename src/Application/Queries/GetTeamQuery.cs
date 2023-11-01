using Application.Contracts;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetTeamQuery : IRequest<Team>
{
    internal Guid Id { get; }

    public GetTeamQuery(Guid id)
    {
        Id = id;
    }
}

public class FindTeamQueryHandler : IRequestHandler<GetTeamQuery, Team>
{
    private readonly IUnitOfWork _uow;

    public FindTeamQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<Team> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var team = 
            await _uow.TeamRepository.GetAsync(request.Id) 
            ?? throw new NotFoundException($"Team with ID {request.Id} not found.");
        
        return team;
    }
}