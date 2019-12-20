using PortainerClient.Config;

namespace PortainerClient.Helpers
{
    public class ConfigModel
    {
        public string Url { get; set; }
        public string Token { get; set; }

        public static ConfigModel Load() => ConfigHelpers.Load();
    }
}