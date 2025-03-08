using CohensionDemostration.Application.Commands;
using CohensionDemostration.Application.Queries;
using CohensionDemostration.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CohensionDemostration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestController : ControllerBase
{
    public readonly IMediator _mediator;
    public readonly IMailService _mailService;

    public ServiceRequestController(IMediator mediator, IMailService mailService)
    {
        _mediator = mediator;
        _mailService = mailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var serviceRequest = await _mediator.Send(new GetServiceRequestsQuery());
        return Ok(serviceRequest);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var serviceRequest = await _mediator.Send(new GetServiceRequestQuery(id));
        return Ok(serviceRequest);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceRequestCommand command)
    {
        var serviceRequest = await _mediator.Send(command);
        return Ok(serviceRequest);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateServiceRequestCommand command)
    {
        var serviceRequest = await _mediator.Send(command);
        return Ok(serviceRequest);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteServiceRequestCommand(id));
        return NoContent();
    }
}
