namespace BeTherServer.Services.CreditsService
{
    public interface ICreditsService
    {
        Task UpdateUsersCredits(string i_UserName, int i_Credits);
    }
}
