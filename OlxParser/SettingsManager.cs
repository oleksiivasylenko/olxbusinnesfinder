using Newtonsoft.Json;
using System;
using System.IO;

namespace OlxParser
{
    public static class SettingsManager
    {

        public static Settings GetSettings()
        {
            var fileData = File.ReadAllText($"{GetAppDataFolder()}/Settings.json");
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
            File.WriteAllText($"{GetAppDataFolder()}/Settings.json", JsonConvert.SerializeObject(settings));
        }
    }
}
