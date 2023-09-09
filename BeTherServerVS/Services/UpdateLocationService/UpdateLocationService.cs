using System.Text;
using Amazon.Runtime.Internal.Transform;
using BeTherServer.Models;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BeTherServer.Services.UpdateLocationService
{
    public class UpdateLocationService : IUpdateLocationService
    {
        private readonly Dictionary<string, LocationData> m_CoordinatesLocations = new Dictionary<string, LocationData>();
        private readonly Dictionary<string, string> m_CityLocations = new Dictionary<string, string>();

        private static readonly object sr_DictionaryCoordinatesLock = new object();
        private static readonly object sr_DictionaryCityLock = new object();

        public Dictionary<string, LocationData> GetLocations()
        {
            return m_CoordinatesLocations;
        }

        public Dictionary<string, string> GetCityLocations()
        {
            return m_CityLocations;
        }

        public void UpdateCurrentLocation(string i_UserName, LocationData i_Location, string i_City)
        {
            lock (sr_DictionaryCoordinatesLock)
            {
                if (!m_CoordinatesLocations.ContainsKey(i_UserName))
                {
                    m_CoordinatesLocations.Add(i_UserName, i_Location);
                }
                else
                {
                    m_CoordinatesLocations[i_UserName] = i_Location;
                }

            }

            lock (sr_DictionaryCityLock)
            {
                if (!m_CityLocations.ContainsKey(i_UserName))
                {
                    m_CityLocations.Add(i_UserName, i_City);
                }
                else
                {
                    m_CityLocations[i_UserName] = i_City;
                }

            }
        }

    }
}
