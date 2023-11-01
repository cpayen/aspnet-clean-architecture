using Application.Contracts;
using Application.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetPlayerQuery : IRequest<Player>
{
    internal Guid Id { get; }

    public GetPlayerQuery(Guid id)
    {
        Id = id;
    }
}

public class FindPlayerQueryHandler : IRequestHandler<GetPlayerQuery, Player>
{
    private readonly IUnitOfWork _uow;

    public FindPlayerQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<Player> Handle(GetPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = 
            await _uow.PlayerRepository.GetAsync(request.Id) 
            ?? throw new NotFoundException($"Player with ID {request.Id} not found.");
        
        return player;
    }
}