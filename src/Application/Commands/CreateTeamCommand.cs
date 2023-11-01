using Application.Contracts;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class CreateTeamCommand : IRequest<Team>
{
    internal string Name { get; }

    public CreateTeamCommand(string name)
    {
        Name = name;
    }
}

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Team>
{
    private readonly IUnitOfWork _uow;

    public CreateTeamCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var newId = await _uow.TeamRepository.CreateAsync(new Team()
        {
            Name = request.Name
        });
        
        await _uow.SaveAsync();

        var team = await _uow.TeamRepository.GetAsync(newId);
        return team!;
    }
}

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(250);
    }
}