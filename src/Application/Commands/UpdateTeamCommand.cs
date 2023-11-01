using Application.Contracts;
using Application.Exceptions;
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
    private readonly IUnitOfWork _uow;

    public UpdateTeamCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<Team> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = 
            await _uow.TeamRepository.GetAsync(request.Id) 
            ?? throw new NotFoundException($"Team with ID {request.Id} not found.");

        team.Name = request.Name;
        
        await _uow.TeamRepository.UpdateAsync(team);
        await _uow.SaveAsync();
        
        return team;
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