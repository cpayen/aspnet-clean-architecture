using Application.Contracts;
using Application.Exceptions;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class CreatePlayerCommand : IRequest<Player>
{
    internal string Name { get; }
    internal Guid TeamId { get; }
    internal int Number { get; }

    public CreatePlayerCommand(string name, Guid teamId, int number)
    {
        Name = name;
        TeamId = teamId;
        Number = number;
    }
}

public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Player>
{
    private readonly IUnitOfWork _uow;
    private readonly PlayerService _playerService;

    public CreatePlayerCommandHandler(IUnitOfWork uow, PlayerService playerService)
    {
        _uow = uow;
        _playerService = playerService;
    }

    public async Task<Player> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var team = 
            await _uow.TeamRepository.GetAsync(request.TeamId)
            ?? throw new BadRequestException($"Team with ID {request.TeamId} does not exist.");

        var numberIsAvailable = await _playerService.CheckNumberIsAvailableForTeam(request.Number, request.TeamId);
        if (!numberIsAvailable)
        {
            throw new UnavailablePlayerNumberException(request.Number, team.Name);
        }
        
        var newId = await _uow.PlayerRepository.CreateAsync(new Player()
        {
            Name = request.Name,
            Team = team,
            Number = request.Number
        });
        
        await _uow.SaveAsync();

        var player = await _uow.PlayerRepository.GetAsync(newId);
        return player!;
    }
}

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(250);
        RuleFor(x => x.Number).GreaterThanOrEqualTo(0).LessThanOrEqualTo(99);
    }
}