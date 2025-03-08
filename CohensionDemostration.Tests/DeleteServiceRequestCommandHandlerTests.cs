using CohensionDemostration.Application.Commands;
using CohensionDemostration.Application.Services;
using CohensionDemostration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CohensionDemostration.Tests
{
    public class DeleteServiceRequestCommandHandlerTests : BaseTest
    {
        [Fact]
        public async Task Handler_WithCorrectRequest_ShouldReturnCorrect()
        {
            //Arrange
            var idServiceRequest = MockServiceRequest(); 
            var mockMailService = new Mock<IMailService>();
            mockMailService
                .Setup(s => s.SendNotificationEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.CompletedTask);
            var handler = new DeleteServiceRequestCommandHandler(_context, mockMailService.Object);
            var command = new DeleteServiceRequestCommand(idServiceRequest);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            var createdServiceRequest = await _context.Services.FirstOrDefaultAsync(s => s.Id == idServiceRequest);
            Assert.Null(createdServiceRequest);
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
            var handler = new DeleteServiceRequestCommandHandler(_context, mockMailService.Object);
            var command = new DeleteServiceRequestCommand(Guid.NewGuid());

            //Act
            await Assert.ThrowsAsync<Exception>(async() => await handler.Handle(command, CancellationToken.None));

        }
    }
}