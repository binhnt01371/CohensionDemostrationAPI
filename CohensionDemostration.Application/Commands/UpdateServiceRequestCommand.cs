using CohensionDemostration.Application.Constants;
using CohensionDemostration.Application.Services;
using CohensionDemostration.Domain.Entities;
using CohensionDemostration.Domain.Enums;
using CohensionDemostration.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Application.Commands;

public class UpdateServiceRequestCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string BuildingCode { get; internal set; }
    public string Description { get; internal set; }
    public CurrentStatusEnum CurrentStatus { get; internal set; }
    public string LastModifiedBy { get; internal set; }
    public DateTime LastModifiedDate { get; internal set; }

    public UpdateServiceRequestCommand(Guid id, 
        string buildingCode, 
        string description, 
        CurrentStatusEnum currentStatus, 
        string lastModifiedBy, 
        DateTime lastModifiedDate)
    {
        Id = id;
        BuildingCode = buildingCode;
        Description = description;
        CurrentStatus = currentStatus;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;
    }
}

public class UpdateServiceRequestCommandHandler : IRequestHandler<UpdateServiceRequestCommand, Guid>
{
    private readonly CohensionDbContext _context;
    public readonly IMailService _mailService;
    public UpdateServiceRequestCommandHandler(CohensionDbContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    public async Task<Guid> Handle(UpdateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        var updateServiceRequest = await _context.Services.FirstOrDefaultAsync(s=> s.Id == request.Id);
        if (updateServiceRequest == null) throw new Exception("ServiceRequest does not exist!");
        updateServiceRequest.BuildingCode = request.BuildingCode;
        updateServiceRequest.Description = request.Description;
        updateServiceRequest.CurrentStatus = request.CurrentStatus;
        updateServiceRequest.LastModifiedBy = request.LastModifiedBy;
        updateServiceRequest.LastModifiedDate = request.LastModifiedDate;

        await _context.SaveChangesAsync();
        await _mailService.SendNotificationEmailAsync(
            updateServiceRequest.OwnerEmail,
            ServiceRequestConstant.ServiceRequestNotificationSubject,
            $"Service request for Building {updateServiceRequest.BuildingCode} is updated");
        return updateServiceRequest.Id;
    }
}
