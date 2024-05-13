using Microsoft.AspNetCore.SignalR;

namespace Announcement
{
    public class NotificationHub : Hub
    {
        public async Task SendAnnouncement(string announcement, string? userId = null)
        {
            if (userId == null)
            {
                // Broadcast to all connected clients
                await Clients.All.SendAsync("ReceiveAnnouncement", announcement);
            }
            else
            {
                // Send to a specific user
                await Clients.User(userId).SendAsync("ReceiveAnnouncement", announcement);
            }
        }
    }
}