using BeTherServer.Models;

namespace BeTherServer.Services.UpdateLocationService
{
    public class UpdateLocationService : IUpdateLocationService
    {
        private readonly Dictionary<string, Location> m_Locations = new Dictionary<string, Location>();
        private static readonly object sr_DictionaryLock = new object();

        public Dictionary<string, Location> GetLocations()
        {
             return m_Locations;
        }

        public void UpdateCurrentLocation(string i_UserName, Location i_Location)
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
