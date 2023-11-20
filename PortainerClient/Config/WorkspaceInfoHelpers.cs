using System;
using System.IO;
using System.Text.Json;

namespace PortainerClient.Config
{
    /// <summary>
    /// Helpers for workspace info model
    /// </summary>
    public static class WorkspaceInfoHelpers
    {
        private static string WorkspaceInfoFilePath { get; } =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                Consts.WorkspaceInfoFileName);

        private static WorkspaceInfoModel? _currentWorkspaceInfoModel;

        /// <summary>
        /// Saves configuration to file
        /// </summary>
        /// <param name="config">Configuration instance</param>
        public static void Save(this WorkspaceInfoModel config)
        {
            var jsonText = JsonSerializer.Serialize(config);
            File.WriteAllText(WorkspaceInfoFilePath, jsonText);
            _currentWorkspaceInfoModel = config;
        }

        /// <summary>
        /// Loads configuration from file
        /// </summary>
        /// <returns>Configuration model instance</returns>
        /// <exception cref="Exception">Occurs when configuration file is not found</exception>
        public static WorkspaceInfoModel Load()
        {
            if (_currentWorkspaceInfoModel != null)
                return _currentWorkspaceInfoModel;

            if (!File.Exists(WorkspaceInfoFilePath))
                throw new Exception(
                    "Workspace info file is not found. Before execute any command you should to authorize first.");

            var jsonText = File.ReadAllText(WorkspaceInfoFilePath);
            _currentWorkspaceInfoModel = JsonSerializer.Deserialize<WorkspaceInfoModel>(jsonText);
            return _currentWorkspaceInfoModel ?? throw new InvalidOperationException("Workspace info model is empty");
        }
    }
}
