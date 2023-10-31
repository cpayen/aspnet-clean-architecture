using Application.Contracts;
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
    private readonly IUnitOfWork _uow;

    public DeleteTeamCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
    {
        await _uow.TeamRepository.DeleteAsync(request.Id);
        await _uow.SaveAsync();
    }
}

public class DeleteTeamCommandValidator : AbstractValidator<DeleteTeamCommand>
{
    public DeleteTeamCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}