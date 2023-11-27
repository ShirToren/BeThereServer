using BeTherServer.Services.Utils;

namespace BeTherServer.Services.UserRoomsService
{
    public interface IUserRoomsService
    {
        Task<ResultUnit<string>> InsertUserRoom(string i_UserName, string i_ChatRoomId);
        Task<ResultUnit<HashSet<string>>> GetChatRoomsByUserName(string i_UserName);
    }
}
