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
        return Ok(result);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Find(Guid id)
    {
        var query = new FindTeamQuery(id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
    {
        var command = new CreateTeamCommand(dto.Name);
        var result = await _mediator.Send(command);
        return Created(result.Id.ToString(), result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTeamDto dto)
    {
        var command = new UpdateTeamCommand(id, dto.Name);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteTeamCommand(id);
        await _mediator.Send(command);
        return NoContent();
    }
}