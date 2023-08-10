using BeTherServer.Models;

namespace BeTherServer.Services.UpdateLocationService
{
    public interface IUpdateLocationService
    {
        void UpdateCurrentLocation(string i_UserName, LocationData i_Location);
        Dictionary<string, LocationData> GetLocations();
    }
}
