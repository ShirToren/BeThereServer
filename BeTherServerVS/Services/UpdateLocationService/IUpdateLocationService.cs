using BeTherServer.Models;

namespace BeTherServer.Services.UpdateLocationService
{
    public interface IUpdateLocationService
    {
        void UpdateCurrentLocation(string i_UserName, Location i_Location);
        Dictionary<string, Location> GetLocations();
    }
}
