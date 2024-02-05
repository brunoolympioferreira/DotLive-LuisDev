using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRChatApp.API.Hubs;
using SignalRChatApp.API.Model;

namespace SignalRChatApp.API.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationsController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostNotification(GroupNotificationInputModel model)
        {
            await _hubContext.Clients.Group(model.GroupName).SendAsync("ReceiveMessage",
                "Administrador",
                model.Message);

            return NoContent();
        }
    }
}
