using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.UserRoomsService
{
    public class UserRoomsService : IUserRoomsService
    {
        private IUserRoomsDBContext m_UserRoomsDataBaseService;
        public UserRoomsService(IUserRoomsDBContext i_UserRoomsDataBaseService)
        {
            m_UserRoomsDataBaseService = i_UserRoomsDataBaseService;
        }
        public async Task<ResultUnit<string>> InsertUserRoom(string i_UserName, string i_ChatRoomId)
        {
            ResultUnit<string> result = new ResultUnit<string>();

            await m_UserRoomsDataBaseService.InsertUserRoom(i_UserName, i_ChatRoomId);

            result.IsSuccess = true;
            return result;
        }

        public async Task<ResultUnit<HashSet<string>>> GetChatRoomsByUserName(string i_UserName)
        {
            ResultUnit<HashSet<string>> result = new ResultUnit<HashSet<string>>();
            HashSet<string> set = await m_UserRoomsDataBaseService.GetRoomsByUserName(i_UserName);

            if (set != null)
            {
                result.IsSuccess = true;
                result.ReturnValue = set;
            }
            else
            {
                result.IsSuccess = true;
                result.ReturnValue = new HashSet<string>();
            }
            return result;
        }
    }
}
