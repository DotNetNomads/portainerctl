using System;
using System.IO;
using PortainerClient.Helpers;
using RestSharp;

namespace PortainerClient.Config
{
    /// <summary>
    /// Helpers for configuration model
    /// </summary>
    public static class ConfigHelpers
    {
        private static string ConfigFilePath { get; } =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Consts.ConfigFileName);

        private static ConfigModel _currentConfigModel;

        /// <summary>
        /// Saves configuration to file
        /// </summary>
        /// <param name="config">Configuration instance</param>
        public static void Save(this ConfigModel config)
        {
            var jsonText = SimpleJson.SerializeObject(config);
            File.WriteAllText(ConfigFilePath, jsonText);
            _currentConfigModel = config;
        }

        /// <summary>
        /// Loads configuration from file
        /// </summary>
        /// <returns>Configuration model instance</returns>
        /// <exception cref="Exception">Occurs when configuration file is not found</exception>
        public static ConfigModel Load()
        {
            if (_currentConfigModel != null)
                return _currentConfigModel;

            if (!File.Exists(ConfigFilePath))
                throw new Exception(
                    "Configuration file is not found. Before execute any command you should to authorize first.");

            var jsonText = File.ReadAllText(ConfigFilePath);
            return _currentConfigModel ??= SimpleJson.DeserializeObject<ConfigModel>(jsonText);
        }
    }
}
