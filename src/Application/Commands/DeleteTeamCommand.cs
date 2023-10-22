using Application.Contracts;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class DeleteTeamCommand : IRequest
{
    internal Guid Id { get; }

    public DeleteTeamCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand>
{
    private readonly ITeamRepository _teamRepository;

    public DeleteTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _teamRepository.Delete(request.Id);
        await Task.Run(() => team, cancellationToken);
    }
}

public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}