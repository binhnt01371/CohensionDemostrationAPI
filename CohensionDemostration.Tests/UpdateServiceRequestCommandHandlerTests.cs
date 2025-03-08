using CohensionDemostration.Application.Commands;
using CohensionDemostration.Application.Services;
using CohensionDemostration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CohensionDemostration.Tests
{
    public class UpdateServiceRequestCommandHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handler_WithCorrectRequest_ShouldReturnServiceRequestId()
        {
            //Arrange
            var idServiceRequest = MockServiceRequest();
            var mockMailService = new Mock<IMailService>();
            mockMailService
                .Setup(s => s.SendNotificationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var handler = new UpdateServiceRequestCommandHandler(_context, mockMailService.Object);
            var command = new UpdateServiceRequestCommand(
                idServiceRequest,
                "002",
                "002 Description",
                Domain.Enums.CurrentStatusEnum.InProgress,
                "User",
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
            Assert.Equal(createdServiceRequest.LastModifiedBy, command.LastModifiedBy);
            Assert.Equal(createdServiceRequest.LastModifiedDate, command.LastModifiedDate);
        }

        [Fact]
        public async Task Handler_WithInCorrectRequest_ShouldThrowException()
        {
            //Arrange
            var idServiceRequest = MockServiceRequest(); 
            var mockMailService = new Mock<IMailService>();
            mockMailService
                .Setup(s => s.SendNotificationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var handler = new UpdateServiceRequestCommandHandler(_context, mockMailService.Object);
            var command = new UpdateServiceRequestCommand(
                Guid.NewGuid(),
                "002",
                "002 Description",
                Domain.Enums.CurrentStatusEnum.InProgress,
                "User",
                DateTime.Now);

            //Act
            await Assert.ThrowsAsync<Exception>(async() => await handler.Handle(command, CancellationToken.None));

        }
    }
}