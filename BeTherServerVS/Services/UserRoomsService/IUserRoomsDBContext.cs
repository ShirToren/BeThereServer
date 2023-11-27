namespace BeTherServer.Services.UserRoomsService
{
    public interface IUserRoomsDBContext
    {
        Task InsertUserRoom(string i_UserName, string i_ChatRoomId);
        Task<HashSet<string>> GetRoomsByUserName(string i_Username);

    }
}
