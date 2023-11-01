using Application.Contracts;
using Application.Exceptions;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Commands;

public class UpdatePlayerCommand : IRequest<Player>
{
    internal Guid Id { get; }
    internal string Name { get; }
    internal Guid TeamId { get; }
    internal int Number { get; }

    public UpdatePlayerCommand(Guid id, string name, Guid teamId, int number)
    {
        Id = id;
        Name = name;
        TeamId = teamId;
        Number = number;
    }
}

public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, Player>
{
    private readonly IUnitOfWork _uow;
    private readonly PlayerService _playerService;

    public UpdatePlayerCommandHandler(IUnitOfWork uow, PlayerService playerService)
    {
        _uow = uow;
        _playerService = playerService;
    }

    public async Task<Player> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = 
            await _uow.PlayerRepository.GetAsync(request.Id)
            ?? throw new NotFoundException($"Player with ID {request.Id} not found.");
        
        var team = 
            await _uow.TeamRepository.GetAsync(request.TeamId)
            ?? throw new BadRequestException($"Team with ID {request.TeamId} does not exist.");
        
        var numberIsAvailable = await _playerService.CheckNumberIsAvailableForTeam(request.Number, request.TeamId);
        if (!numberIsAvailable)
        {
            throw new UnavailablePlayerNumberException(request.Number, team.Name);
        }

        player.Name = request.Name;
        player.Team = team;
        player.Number = request.Number;
        
        await _uow.PlayerRepository.UpdateAsync(player);
        await _uow.SaveAsync();

        return player;
    }
}

public class UpdatePlayerCommandValidator : AbstractValidator<UpdatePlayerCommand>
{
    public UpdatePlayerCommandValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.Name).MinimumLength(1).MaximumLength(250);
        RuleFor(x => x.TeamId).NotEqual(Guid.Empty);
        RuleFor(x => x.Number).GreaterThanOrEqualTo(0).LessThanOrEqualTo(99);
    }
}