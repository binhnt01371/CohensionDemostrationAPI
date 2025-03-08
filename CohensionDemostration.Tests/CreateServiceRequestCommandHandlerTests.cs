using CohensionDemostration.Application.Commands;
using CohensionDemostration.Application.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CohensionDemostration.Tests
{
    public class CreateServiceRequestCommandHandlerTests: BaseTest
    {
        [Fact]
        public async Task Handler_WithCorrectRequest_ShouldReturnServiceRequestId()
        {
            //Arrange
            var mockMailService = new Mock<IMailService>();
            mockMailService
                .Setup(s=>s.SendNotificationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var handler = new CreateServiceRequestCommandHandler(_context, mockMailService.Object);
            var command = new CreateServiceRequestCommand(
                "001",
                "001 Description",
                "owner@mail.com",
                Domain.Enums.CurrentStatusEnum.Created,
                "Admin",
                DateTime.Now,
                "Admin",
                DateTime.Now);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.IsType<Guid>(result);
            var createdServiceRequest = await _context.Services.FirstOrDefaultAsync(s => s.Id == result);
            Assert.NotNull(createdServiceRequest);
            Assert.Equal(createdServiceRequest.BuildingCode, command.BuildingCode);
            Assert.Equal(createdServiceRequest.Description, command.Description);
            Assert.Equal(createdServiceRequest.CurrentStatus, command.CurrentStatus);
        }
    }
}