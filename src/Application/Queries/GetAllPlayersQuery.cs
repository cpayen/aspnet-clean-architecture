using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllPlayersQuery : IRequest<IEnumerable<Player>> {}

public class GetAllPlayersQueryHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<Player>>
{
    private readonly IUnitOfWork _uow;

    public GetAllPlayersQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<Player>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
    {
        return await _uow.PlayerRepository.GetAllAsync();
    }
}