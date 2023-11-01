using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dto;

namespace WebApp.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : Controller
{
    private readonly IMediator _mediator;
    
    public TeamsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllTeamsQuery();
        var result = await _mediator.Send(query);
        return Ok(result.Select(TeamDto.FromDomainEntity));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetTeamQuery(id);
        var result = await _mediator.Send(query);
        return Ok(TeamDto.FromDomainEntity(result));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
    {
        var command = new CreateTeamCommand(dto.Name);
        var result = await _mediator.Send(command);
        return Created(result.Id.ToString(), TeamDto.FromDomainEntity(result));
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeamDto dto)
    {
        var command = new UpdateTeamCommand(id, dto.Name);
        var result = await _mediator.Send(command);
        return Ok(TeamDto.FromDomainEntity(result));
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTeamCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}