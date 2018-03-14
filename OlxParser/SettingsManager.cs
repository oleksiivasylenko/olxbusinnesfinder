using Newtonsoft.Json;
using System;
using System.IO;

namespace OlxParser
{
    public static class SettingsManager
    {
        public static string ActiveSettingsName { get; set; }
        private static string _filePath => $"{GetAppDataFolder()}/{ActiveSettingsName}.json";
        public static Settings GetSettings()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText($"{GetAppDataFolder()}/{ActiveSettingsName}.json", JsonConvert.SerializeObject(new Settings() { LastSavedDate = DateTime.Now } ));

            var fileData = File.ReadAllText(_filePath);
            var settings = JsonConvert.DeserializeObject<Settings>(fileData);
            return settings;
        }

        public static string GetAppDataFolder()
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
            File.WriteAllText($"{GetAppDataFolder()}/{ActiveSettingsName}.json", JsonConvert.SerializeObject(settings));
        }

        public static void ClearSettings()
        {
            File.WriteAllText($"{GetAppDataFolder()}/{ActiveSettingsName}.json", JsonConvert.SerializeObject(new Settings() { LastSavedDate = DateTime.Now }));
        }
    }
}
