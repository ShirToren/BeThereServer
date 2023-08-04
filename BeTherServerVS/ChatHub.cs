using Microsoft.AspNetCore.SignalR;

namespace BeTherServer.Chat;

public class ChatHub : Hub
{
    public ChatHub()
    {

    }

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("MessageRecived", message);
    }
}
