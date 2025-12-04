using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Expenses.Queries.GetExpenseReportsForUser;
using TimesheetManagement.Domain.Expenses;
using TimesheetManagement.Domain.Expenses.Repositories;
using TimesheetManagement.UnitTests.TestHelpers;
using Xunit;

namespace TimesheetManagement.UnitTests.Application.Expenses.Queries.GetExpenseReportsForUser;

public class GetExpenseReportsForUserHandlerTests
{
    private readonly Mock<IExpenseReportRepository> _repoMock;
    private readonly GetExpenseReportsForUserHandler _handler;

    public GetExpenseReportsForUserHandlerTests()
    {
        _repoMock = ApplicationTestHelpers.CreateExpenseReportRepository();
        _handler = new GetExpenseReportsForUserHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ValidQuery_ShouldReturnSummaries()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var reports = new List<ExpenseReport>
        {
            TestData.CreateSampleExpenseReport(userId: userId),
            TestData.CreateSampleExpenseReport(userId: userId)
        };
        _repoMock.Setup(x => x.GetForUserAsync(userId, null, null, It.IsAny<CancellationToken>())).ReturnsAsync(reports);
        var query = new GetExpenseReportsForUserQuery(userId);

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<ExpenseReportSummaryDto>();
    }
}