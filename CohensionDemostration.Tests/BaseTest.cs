using CohensionDemostration.Domain.Entities;
using CohensionDemostration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Tests
{
    public class BaseTest : IDisposable
    {
        protected CohensionDbContext _context;
        public BaseTest()
        {
            var options = new DbContextOptionsBuilder<CohensionDbContext>().UseInMemoryDatabase("CohensionDatabase").Options;
            _context = new CohensionDbContext(options);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Guid MockServiceRequest()
        {
            var idServiceRequest = Guid.NewGuid();
            var newServiceRequest = new ServiceRequest(
                idServiceRequest,
                "001",
                "001 Description",
                "owner@mail.com",
                Domain.Enums.CurrentStatusEnum.Created,
                "Admin",
                DateTime.Now,
                "Admin",
                DateTime.Now);
            _context.Add(newServiceRequest);
            _context.SaveChanges();
            return idServiceRequest;
        }

        public void RemoveAllServiceRequestIfExist()
        {
            var idServiceRequest = _context.Services.ToList();
            _context.RemoveRange(idServiceRequest);
            _context.SaveChanges();
        }
    }
}
