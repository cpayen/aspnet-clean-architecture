using Application.Contracts;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class UpdateTeamCommand : IRequest<Team>
{
    internal Guid Id { get; }
    internal string Name { get; }

    public UpdateTeamCommand(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}

public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Team>
{
    private readonly ITeamRepository _teamRepository;

    public UpdateTeamCommandHandler(ITeamRepository teamRepository)
    {
        _teamRepository = teamRepository;
    }
    
    public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = _teamRepository.Update(new Team()
        {
            Id = request.Id,
            Name = request.Name
        });
        return await Task.Run(() => team, cancellationToken);
    }
}

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
    public UpdateTeamCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(250);
    }
}