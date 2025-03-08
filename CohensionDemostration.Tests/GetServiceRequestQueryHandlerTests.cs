using CohensionDemostration.Application.Commands;
using CohensionDemostration.Application.Queries;
using Microsoft.EntityFrameworkCore;

namespace CohensionDemostration.Tests
{
    public class GetServiceRequestQueryHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handler_WithCorrectRequest_ShouldReturnServiceRequestId()
        {
            //Arrange
            var idServiceRequest = MockServiceRequest();
            var handler = new GetServiceRequestQueryHandler(_context);
            var query = new GetServiceRequestQuery(idServiceRequest);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            var expected = await _context.Services.FirstOrDefaultAsync(s => s.Id == idServiceRequest);
            Assert.NotNull(result);
            Assert.Equal(expected, result);
        }
    }
}