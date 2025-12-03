using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.Hubs;

namespace TimesheetManagement.Services;

public class NotificationSender : INotificationSender
{
    private readonly IHubContext<MyNotificationHub> _hubContext;

    public NotificationSender(IHubContext<MyNotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToUserAsync(Guid userId, string message, CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message, cancellationToken);
    }

    public async Task SendToRoleAsync(string role, string message, CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.Group(role).SendAsync("ReceiveNotification", message, cancellationToken);
    }
}
