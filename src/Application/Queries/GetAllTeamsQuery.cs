using Application.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllTeamsQuery : IRequest<IEnumerable<Team>> {}

public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, IEnumerable<Team>>
{
    private readonly IUnitOfWork _uow;

    public GetAllTeamsQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<IEnumerable<Team>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken)
    {
        return await _uow.TeamRepository.GetAllAsync();
    }
}