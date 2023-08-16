using Amazon.Runtime.Internal;
using BeTherServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Reflection.Metadata;

namespace BeTherServer.Services.ChatService;

public class ChatHub : Hub
{
    private IChatMessagesDBContext m_ChatMessagesDatabaseService;

    public ChatHub(IChatMessagesDBContext i_ChatMessagesDatabaseService)
    {
        m_ChatMessagesDatabaseService = i_ChatMessagesDatabaseService;
    }

    public async Task JoinChatRoom(string chatRoomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);

               var messages = await m_ChatMessagesDatabaseService.GetMessagesByChatRoom(chatRoomId);
               await Clients.Caller.SendAsync("LoadChatHistory", messages);

        //Handle Message History in .NET MAUI:
        //In your .NET MAUI app, handle the "LoadChatHistory" event from SignalR.When the chat history is received, populate the chat interface with the saved messages.
    }
    public async Task LeaveChatRoom(string chatRoomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
    }

    public async Task SendMessage(string chatRoomId, string sender, string message)
    {
        var chatMessage = new ChatMessage
        {
            Sender = sender,
            Content = message,
            Timestamp = DateTime.UtcNow,
            ChatRoomId = chatRoomId
        };

        // Save the message to the data storage
        await m_ChatMessagesDatabaseService.SaveMessage(chatMessage);

        // Broadcast the message to clients
        await Clients.Group(chatRoomId).SendAsync("MessageReceived", chatMessage);
    }
}
