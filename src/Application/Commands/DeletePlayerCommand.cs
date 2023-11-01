using Application.Contracts;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class DeletePlayerCommand : IRequest
{
    internal Guid Id { get; }

    public DeletePlayerCommand(Guid id)
    {
        Id = id;
    }
}

public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand>
{
    private readonly IUnitOfWork _uow;

    public DeletePlayerCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
    {
        await _uow.PlayerRepository.DeleteAsync(request.Id);
        await _uow.SaveAsync();
    }
}

public class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
    }
}