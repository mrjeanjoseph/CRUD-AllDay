using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Infrastructure.Persistence;
using TimesheetManagement.IntegrationTests.TestHelpers;
using TimesheetManagement.Domain.TimeTracking;
using TimesheetManagement.Domain.TimeTracking.Events;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TimesheetManagement.IntegrationTests.Persistence.InMemory;

[Trait("Category","InMemory")]
public class UnitOfWorkInMemoryTests : IClassFixture<InMemoryDatabaseFixture>
{
    private readonly InMemoryDatabaseFixture _fixture;

    public UnitOfWorkInMemoryTests(InMemoryDatabaseFixture fixture) => _fixture = fixture;

    private sealed class FakeDispatcher : IDomainEventDispatcher
    {
        public List<object> Dispatched { get; } = new();

        public Task DispatchAsync(IEnumerable<object> domainEvents, CancellationToken cancellationToken = default)
        {
            Dispatched.AddRange(domainEvents);
            return Task.CompletedTask;
        }
    }

    [Fact]
    public async Task SaveChanges_CommitsTransactionAndDispatchesDomainEvents()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var from = new DateOnly(2024, 2, 1);
        var to = new DateOnly(2024, 2, 7);
        var ts = new TimeSheet(userId, from, to);
        ts.AddEntry(new TimeEntry(Guid.NewGuid(), from, 
            new Domain.TimeTracking.ValueObjects.HoursWorked(8), "Work"));
        ts.Submit(); // raises TimeSheetSubmittedEvent

        await _fixture.DbContext.TimeSheets.AddAsync(ts);

        var fakeDispatcher = new FakeDispatcher();
        var uow = new UnitOfWork(_fixture.DbContext, fakeDispatcher);

        // Act
        var result = await uow.SaveChangesAsync();

        // Assert
        result.Should().BeGreaterThan(0);
        fakeDispatcher.Dispatched.Should().ContainSingle()
            .Which.Should().BeOfType<TimeSheetSubmittedEvent>();

        // Verify persisted
        var loaded = await _fixture.DbContext.TimeSheets
            .Include(s => s.Entries)
            .FirstOrDefaultAsync(s => s.Id == ts.Id);
        loaded.Should().NotBeNull();
        loaded!.Status.Should().Be(TimeSheetStatus.Submitted);
    }
}