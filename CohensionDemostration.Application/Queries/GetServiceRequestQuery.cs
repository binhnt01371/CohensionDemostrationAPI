using CohensionDemostration.Domain.Entities;
using CohensionDemostration.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Application.Queries;

public class GetServiceRequestQuery : IRequest<ServiceRequest>
{
    public Guid Id { get; set; }
	public GetServiceRequestQuery(Guid id)
	{
		Id = id;
	}
}

public class GetServiceRequestQueryHandler : IRequestHandler<GetServiceRequestQuery, ServiceRequest>
{
    private readonly CohensionDbContext _context;
    public GetServiceRequestQueryHandler(CohensionDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceRequest> Handle(GetServiceRequestQuery request, CancellationToken cancellationToken)
    {
        var serviceRequest = await _context.Services.FirstOrDefaultAsync(s => s.Id == request.Id);
        if (serviceRequest == null) throw new Exception("ServiceRequest does not exist!");
        return serviceRequest;
    }
}
