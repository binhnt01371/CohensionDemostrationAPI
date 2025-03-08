using CohensionDemostration.Application.Constants;
using CohensionDemostration.Application.Services;
using CohensionDemostration.Domain.Entities;
using CohensionDemostration.Domain.Enums;
using CohensionDemostration.Infrastructure.Data;
using MediatR;

namespace CohensionDemostration.Application.Commands;

public class CreateServiceRequestCommand : IRequest<Guid>
{
    public string BuildingCode { get; internal set; }
    public string Description { get; internal set; }
    public string OwnerEmail { get; set; }
    public CurrentStatusEnum CurrentStatus { get; internal set; }
    public string CreatedBy { get; internal set; }
    public DateTime CreatedDate { get; internal set; }
    public string LastModifiedBy { get; internal set; }
    public DateTime LastModifiedDate { get; internal set; }

    public CreateServiceRequestCommand( 
        string buildingCode, 
        string description, 
        string ownerEmail,
        CurrentStatusEnum currentStatus, 
        string createdBy, 
        DateTime createdDate, 
        string lastModifiedBy, 
        DateTime lastModifiedDate)
    {
        BuildingCode = buildingCode;
        Description = description;
        OwnerEmail = ownerEmail;
        CurrentStatus = currentStatus;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;
    }
}

public class CreateServiceRequestCommandHandler : IRequestHandler<CreateServiceRequestCommand, Guid>
{
    private readonly CohensionDbContext _context;
    public readonly IMailService _mailService;
    public CreateServiceRequestCommandHandler(CohensionDbContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    public async Task<Guid> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        var serviceRequest = new ServiceRequest(
            Guid.NewGuid(),
            request.BuildingCode,
            request.Description,
            request.OwnerEmail,
            request.CurrentStatus,
            request.CreatedBy,
            request.CreatedDate,
            request.LastModifiedBy,
            request.LastModifiedDate);
        _context.Add(serviceRequest);

        await _context.SaveChangesAsync();
        await _mailService.SendNotificationEmailAsync(
            serviceRequest.OwnerEmail, 
            ServiceRequestConstant.ServiceRequestNotificationSubject,
            $"Service request for Building {serviceRequest.BuildingCode} is created");
        return serviceRequest.Id;
    }
}
