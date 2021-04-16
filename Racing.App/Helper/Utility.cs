using System.Configuration;

namespace Racing.App.Helper
{
    public static class Utility
    {
        public static string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        public static string GetResourceValue(string key)
        {
            return RacingVehicle.ResourceManager.GetString(key);
        }
    }
}