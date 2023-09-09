using BeTherServer.Models;

namespace BeTherServer.Services.UpdateLocationService
{
    public interface IUpdateLocationService
    {
        void UpdateCurrentLocation(string i_UserName, LocationData i_Location, string i_City);
        Dictionary<string, LocationData> GetLocations();
        Dictionary<string, string> GetCityLocations();
    }
}
