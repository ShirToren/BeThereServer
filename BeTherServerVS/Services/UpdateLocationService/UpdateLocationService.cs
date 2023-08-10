using BeTherServer.Models;

namespace BeTherServer.Services.UpdateLocationService
{
    public class UpdateLocationService : IUpdateLocationService
    {
        private readonly Dictionary<string, LocationData> m_Locations = new Dictionary<string, LocationData>();
        private static readonly object sr_DictionaryLock = new object();

        public Dictionary<string, LocationData> GetLocations()
        {
             return m_Locations;
        }

        public void UpdateCurrentLocation(string i_UserName, LocationData i_Location)
        {
            lock (sr_DictionaryLock)
            {
                if(!m_Locations.ContainsKey(i_UserName))
                {
                    m_Locations.Add(i_UserName, i_Location);
                } else
                {
                    m_Locations[i_UserName] = i_Location;
                }
               
            }
        }
        
    }
}
