using Newtonsoft.Json;
using System;
using System.IO;

namespace OlxParser
{
    public static class SettingsManager
    {
        private static string _filePath => $"{GetAppDataFolder()}/Settings.json";
        public static Settings GetSettings()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText($"{GetAppDataFolder()}/Settings.json", JsonConvert.SerializeObject(new Settings() { LastSavedDate = DateTime.Now } ));

            var fileData = File.ReadAllText(_filePath);
            var settings = JsonConvert.DeserializeObject<Settings>(fileData);
            return settings;
        }

        private static string GetAppDataFolder()
        {
            string dirName = AppDomain.CurrentDomain.BaseDirectory; // Starting Dir
            FileInfo fileInfo = new FileInfo(dirName);
            DirectoryInfo parentDir = fileInfo.Directory.Parent.Parent;
            string parentDirName = parentDir.FullName;

            return parentDirName + "\\App_Data\\";
        }

        public static void SaveSettings(Settings settings)
        {
            settings.LastSavedDate = DateTime.Now;
            File.WriteAllText($"{GetAppDataFolder()}/Settings.json", JsonConvert.SerializeObject(settings));
        }
    }
}
