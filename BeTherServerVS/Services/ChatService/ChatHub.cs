using Amazon.Runtime.Internal;
using BeTherServer.Models;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Driver.Core.Connections;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace BeTherServer.Services.ChatService;

public class ChatHub : Hub
{
    private IChatMessagesDBContext m_ChatMessagesDatabaseService;
    private readonly Dictionary<string, HashSet<string>> userRooms = new Dictionary<string, HashSet<string>>();

    public ChatHub(IChatMessagesDBContext i_ChatMessagesDatabaseService)
    {
        m_ChatMessagesDatabaseService = i_ChatMessagesDatabaseService;
        if(userRooms.Count == 0)
        {
            //fetch from data base
        }
    }

    public async Task JoinChatRoom(string chatRoomId)
    {
        var connectionId = Context.ConnectionId;
        

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

            //await Clients.Client(connectionId).SendAsync("UpdateChatRooms", userRooms[connectionId]);
            await Clients.Group(chatRoomId).SendAsync("UpdateChatRooms", chatRoomId);


            //await Clients.Group(chatRoomId).SendAsync("UserJoined", $"{connectionId} joined the chat room.");
            var messages = await m_ChatMessagesDatabaseService.GetMessagesByChatRoom(chatRoomId);
            await Clients.Caller.SendAsync("LoadChatHistory", messages);
        }
        else
        {
            // User is already in the room
            //await Clients.Caller.SendAsync("AlreadyJoined", "You are already in the chat room.");
        }



        //await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);

        //      var messages = await m_ChatMessagesDatabaseService.GetMessagesByChatRoom(chatRoomId);
        //      await Clients.Caller.SendAsync("LoadChatHistory", messages);

        //Handle Message History in .NET MAUI:
        //In your .NET MAUI app, handle the "LoadChatHistory" event from SignalR.When the chat history is received, populate the chat interface with the saved messages.
    }
    public async Task LeaveChatRoom(string chatRoomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
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

        // Check if the user has already joined the room
        if (!userRooms.ContainsKey(connectionId) || !userRooms[connectionId].Contains(chatRoomId))
        {
            await Groups.AddToGroupAsync(connectionId, chatRoomId);

            if (!userRooms.ContainsKey(connectionId))
            {
                userRooms[connectionId] = new HashSet<string>();
            }

            userRooms[connectionId].Add(chatRoomId);
        }
    }
}
