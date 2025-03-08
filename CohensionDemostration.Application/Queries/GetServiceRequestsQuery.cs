using CohensionDemostration.Domain.Entities;
using CohensionDemostration.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Application.Queries;

public class GetServiceRequestsQuery : IRequest<IEnumerable<ServiceRequest>>
{
}

public class GetServiceRequestsQueryHandler : IRequestHandler<GetServiceRequestsQuery, IEnumerable<ServiceRequest>>
{
    private readonly CohensionDbContext _context;
    public GetServiceRequestsQueryHandler(CohensionDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceRequest>> Handle(GetServiceRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Services.ToListAsync(cancellationToken);
    }
}
