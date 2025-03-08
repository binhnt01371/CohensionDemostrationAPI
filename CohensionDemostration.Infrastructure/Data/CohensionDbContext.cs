using CohensionDemostration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Infrastructure.Data;
public class CohensionDbContext : DbContext
{
    public CohensionDbContext(DbContextOptions<CohensionDbContext> options) : base(options) { }
    public DbSet<ServiceRequest> Services { get; set; }
}
