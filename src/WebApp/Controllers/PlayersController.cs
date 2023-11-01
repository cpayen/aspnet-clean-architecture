using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dto;

namespace WebApp.Controllers;

[ApiController]
[Route("api/players")]
public class PlayersController : Controller
{
    private readonly IMediator _mediator;
    
    public PlayersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllPlayersQuery();
        var result = await _mediator.Send(query);
        return Ok(result.Select(PlayerDto.FromDomainEntity));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetPlayerQuery(id);
        var result = await _mediator.Send(query);
        return Ok(PlayerDto.FromDomainEntity(result));
    }
    
    [HttpGet("team/{teamId:guid}")]
    public async Task<IActionResult> GetAllByTeam(Guid teamId)
    {
        var query = new GetPlayersByTeamQuery(teamId);
        var result = await _mediator.Send(query);
        return Ok(result.Select(PlayerDto.FromDomainEntity));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerDto dto)
    {
        var command = new CreatePlayerCommand(dto.Name, dto.TeamId, dto.Number);
        var result = await _mediator.Send(command);
        return Created(result.Id.ToString(), PlayerDto.FromDomainEntity(result));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlayerDto dto)
    {
        var command = new UpdatePlayerCommand(id, dto.Name, dto.TeamId, dto.Number);
        var result = await _mediator.Send(command);
        return Ok(PlayerDto.FromDomainEntity(result));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeletePlayerCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}