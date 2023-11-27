namespace BeTherServer.Services.CreditsService
{
    public class CreditsService : ICreditsService
    {
        private readonly IUserDBContext r_UserDatabaseService;
        public CreditsService(IUserDBContext i_UserDatabaseService)
        {
            r_UserDatabaseService = i_UserDatabaseService;
        }

        public async Task UpdateUsersCredits(string i_UserName, int i_Credits)
        {
            await r_UserDatabaseService.UpdateUsersCreditsAsync(i_UserName, i_Credits);
        }
    }
}
