using CohensionDemostration.Application.Constants;
using CohensionDemostration.Application.Services;
using CohensionDemostration.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Application.Commands;

public class DeleteServiceRequestCommand : IRequest
{
    public Guid Id { get; internal set; }
    public DeleteServiceRequestCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteServiceRequestCommandHandler : IRequestHandler<DeleteServiceRequestCommand>
{
    private readonly CohensionDbContext _context;
    public readonly IMailService _mailService;
    public DeleteServiceRequestCommandHandler(CohensionDbContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    public async Task Handle(DeleteServiceRequestCommand request, CancellationToken cancellationToken)
    {
        var deleteServiceRequest = await _context.Services.FirstOrDefaultAsync(s => s.Id == request.Id);
        if (deleteServiceRequest == null) throw new Exception("ServiceRequest does not exist!");
        _context.Services.Remove(deleteServiceRequest);
        await _context.SaveChangesAsync();

        await _mailService.SendNotificationEmailAsync(
            deleteServiceRequest.OwnerEmail,
            ServiceRequestConstant.ServiceRequestNotificationSubject,
            $"Service request for Building {deleteServiceRequest.BuildingCode} is deleted");
    }
}
