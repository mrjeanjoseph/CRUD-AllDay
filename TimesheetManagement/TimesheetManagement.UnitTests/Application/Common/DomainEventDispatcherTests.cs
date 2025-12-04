using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimesheetManagement.Infrastructure.Services;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Common;

public class DomainEventDispatcherTests
{
    [Fact]
    public async Task DispatchAsync_ShouldLogEachEvent()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<DomainEventDispatcher>>();
        var dispatcher = new DomainEventDispatcher(loggerMock.Object);
        var events = new List<object> { new TestEvent(), new TestEvent() };

        // Act
        await dispatcher.DispatchAsync(events);

        // Assert
        loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Exactly(events.Count));
    }

    private class TestEvent { }
}