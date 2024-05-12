using Announcement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HTI_Backend.Controllers
{
  
    public class AnnouncementsController : ApiBaseController

    {
        private readonly IHubContext<NotificationHub> _notificationHub;

        public AnnouncementsController(IHubContext<NotificationHub> notificationHub )
        {
            _notificationHub = notificationHub;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAnnouncement(Announcement__ announcement)
        {
        

            await _notificationHub.Clients.All.SendAsync("NewAnnouncement", announcement);

            return CreatedAtAction(nameof(CreateAnnouncement), new { id = announcement.Id }, announcement);
        }

        // ... other actions (GET, PUT, etc.)
    }


   
    
}

