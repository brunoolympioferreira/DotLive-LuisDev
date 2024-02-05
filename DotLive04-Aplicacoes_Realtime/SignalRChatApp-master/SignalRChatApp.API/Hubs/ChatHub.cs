using Microsoft.AspNetCore.SignalR;

namespace SignalRChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string group, string user, string message)
        {
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.Group(group).SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinGroup(string user, string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
            await Clients.Group(group).SendAsync("ReceiveMessage",
                "Administrador",
                $"{user} Connection: {Context.ConnectionId} se juntou ao grupo {group}");
        }
    }
}
