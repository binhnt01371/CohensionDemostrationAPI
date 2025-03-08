using CohensionDemostration.Application.Commands;
using CohensionDemostration.Application.Queries;
using CohensionDemostration.Domain.Entities;
using CohensionDemostration.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Tests
{
    public class GetServiceRequestsQueryHandlerTests
    {
        protected CohensionDbContext _context;
        [Fact]
        public async Task Handler_WithCorrectRequest_ShouldReturnServiceRequestId()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<CohensionDbContext>()
                .UseInMemoryDatabase("CohensionDatabaseOther")
                .Options;
            _context = new CohensionDbContext(options);
            var expectedRecordNumber = 10;
            for (int i = 0; i < expectedRecordNumber; i++)
            {
                MockServiceRequest();
            }
            var handler = new GetServiceRequestsQueryHandler(_context);
            var query = new GetServiceRequestsQuery();

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.Equal(result.Count(), expectedRecordNumber);
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
    }
}