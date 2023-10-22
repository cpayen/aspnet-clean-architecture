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
    private readonly ITeamRepository _teamRepository;

    public CreateTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }

    public async Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _teamRepository.Create(new Team()
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        });
        return await Task.Run(() => team, cancellationToken);
    }
}

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(250);
    }
}