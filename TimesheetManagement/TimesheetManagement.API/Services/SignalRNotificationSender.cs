using Microsoft.AspNetCore.SignalR;
using TimesheetManagement.Application.Common.Abstractions;
using TimesheetManagement.API.Hubs;

namespace TimesheetManagement.API.Services;
public sealed class SignalRNotificationSender : INotificationSender
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public SignalRNotificationSender(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToUserAsync(Guid userId, string message, CancellationToken cancellationToken = default)
    {
        await _hubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", message, cancellationToken);
    }

    public Task SendToRoleAsync(string role, string message, CancellationToken cancellationToken = default)
    {
        // For simplicity, send to group named after role
        return _hubContext.Clients.Group(role).SendAsync("ReceiveNotification", message, cancellationToken);
    }
}