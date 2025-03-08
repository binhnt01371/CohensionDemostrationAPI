using CohensionDemostration.Domain.Enums;

namespace CohensionDemostration.Domain.Entities;
public class ServiceRequest
{
    public ServiceRequest(Guid id, 
        string buildingCode, 
        string description, 
        string ownerEmail,
        CurrentStatusEnum currentStatus, 
        string createdBy, 
        DateTime createdDate, 
        string lastModifiedBy, 
        DateTime lastModifiedDate)
    {
        Id = id;
        BuildingCode = buildingCode;
        Description = description;
        OwnerEmail = ownerEmail;
        CurrentStatus = currentStatus;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        LastModifiedBy = lastModifiedBy;
        LastModifiedDate = lastModifiedDate;
    }

    public Guid Id { get; set; }
    public string BuildingCode { get; set; }
    public string Description { get; set; }
    public string OwnerEmail { get; set; }
    public CurrentStatusEnum CurrentStatus { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime LastModifiedDate { get; set; }
}

