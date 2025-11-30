using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TimesheetManagement.Hubs
{
    public class MyNotificationHub : Hub
    {
        public async Task BroadcastStatus()
        {
            await Clients.All.SendAsync("displayStatus");
        }

        // Legacy Send method replaced by BroadcastStatus; database notification callback should invoke this via injected IHubContext.
    }
}