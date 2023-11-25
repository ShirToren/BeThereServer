using Amazon.Runtime.Internal;
using BeTherServer.Models;
using BeTherServer.Services.UserRoomsService;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace BeTherServer.Services.ChatService;

public class ChatHub : Hub
{
    private IChatMessagesDBContext m_ChatMessagesDatabaseService;
    private IChatService m_ChatService;

    public ChatHub(IChatMessagesDBContext i_ChatMessagesDatabaseService, IChatService i_ChatService)
    {
        m_ChatMessagesDatabaseService = i_ChatMessagesDatabaseService;
        m_ChatService = i_ChatService;
    }

    public async Task JoinChatRoom(string chatRoomId)
    {
        var connectionId = Context.ConnectionId;
        var userRooms = m_ChatService.GetUserRooms();


        // Check if the user has already joined the room
        if (!userRooms.ContainsKey(connectionId) || !userRooms[connectionId].Contains(chatRoomId))
        {
            await Groups.AddToGroupAsync(connectionId, chatRoomId);

            if (!userRooms.ContainsKey(connectionId))
            {
                userRooms[connectionId] = new HashSet<string>();
            }

            userRooms[connectionId].Add(chatRoomId);

            // Notify clients about the updated list of chat rooms
            await Clients.Group(chatRoomId).SendAsync("UpdateChatRooms", chatRoomId);

        }
        else
        {
            // User is already in the room
            //await Clients.Caller.SendAsync("AlreadyJoined", "You are already in the chat room.");
        }

        await EnterChatRoom(chatRoomId);
    }
    public async Task LeaveChatRoom(string chatRoomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
    }
    public async Task EnterChatRoom(string chatRoomId)
    {
        var messages = await m_ChatMessagesDatabaseService.GetMessagesByChatRoom(chatRoomId);
        await Clients.Caller.SendAsync("LoadChatHistory", messages);
    }

    public async Task<List<ChatMessage>> GetChatMessagesAsync(string chatRoomId)
    {
        return await m_ChatMessagesDatabaseService.GetMessagesByChatRoom(chatRoomId);
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
    public async Task CreateChatRoom(string chatRoomId)
    {
        var connectionId = Context.ConnectionId;
        var userRooms = m_ChatService.GetUserRooms();

        // Check if the user has already joined the room
        //if (!userRooms.ContainsKey(connectionId) || !userRooms[connectionId].Contains(chatRoomId))
        //{
            await Groups.AddToGroupAsync(connectionId, chatRoomId);

/*            if (!userRooms.ContainsKey(connectionId))
            {
                userRooms[connectionId] = new HashSet<string>();
            }

            userRooms[connectionId].Add(chatRoomId);*/
       // }
    }
}
