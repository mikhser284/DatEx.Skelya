using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DatEx.Skelya
{
    public class AppSettings
    {
        public const String ApplicationId = "KustoProductionEventsMonitor";

        public static AppSettings Load()
        {
            String SettingsFileDefaultPath = @$"C:\_AppConfigs\{ApplicationId}\Default\{ApplicationId} - Main.config";
            String sysVarName = $"AppConfigFilePath_{ApplicationId}";
            String settingsFilePath = Environment.GetEnvironmentVariable(sysVarName, EnvironmentVariableTarget.Machine);            
            if (String.IsNullOrEmpty(settingsFilePath))
            {
                try
                {
                    Environment.SetEnvironmentVariable(sysVarName, SettingsFileDefaultPath, EnvironmentVariableTarget.Machine);
                    settingsFilePath = Environment.GetEnvironmentVariable(sysVarName, EnvironmentVariableTarget.Machine);
                }
                catch(Exception) { }
            }
            if (String.IsNullOrEmpty(settingsFilePath))
                settingsFilePath = Environment.GetEnvironmentVariable(sysVarName, EnvironmentVariableTarget.User);
            if (String.IsNullOrEmpty(settingsFilePath))
            {
                Environment.SetEnvironmentVariable(sysVarName, SettingsFileDefaultPath, EnvironmentVariableTarget.User);
                settingsFilePath = Environment.GetEnvironmentVariable(sysVarName, EnvironmentVariableTarget.User);
            }
            if (String.IsNullOrEmpty(settingsFilePath)) throw new KeyNotFoundException($"System variable \"{sysVarName}\" not found");
            String dirName = Path.GetDirectoryName(settingsFilePath);
            if (!Directory.Exists(dirName)) Directory.CreateDirectory(dirName);
            AppSettings settings = null;
            if (!File.Exists(settingsFilePath))
            {
                using (StreamWriter stream = File.CreateText(settingsFilePath))
                {
                    settings = ByDefault();
                    JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings() { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
                    serializer.Serialize(stream, settings);
                }
            }
            else
            {
                using (StreamReader file = File.OpenText(settingsFilePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    settings = (AppSettings)serializer.Deserialize(file, typeof(AppSettings));
                }
            }
            return settings;
        }

        private AppSettings()
        {
            HttpAddressOf = Settings_HttpAddress.ByDefault();
            FolderPathOf = Settings_FolderPath.ByDefault();
        }

        public static AppSettings ByDefault() => new AppSettings();

        [JsonProperty("HttpAddressOf")]
        public Settings_HttpAddress HttpAddressOf { get; set; }

        [JsonProperty("FolderPathOf")]
        public Settings_FolderPath FolderPathOf { get; set; }
    }

    public class Settings_HttpAddress
    {
        [JsonProperty("SkelyaServer")]
        public String SkelyaServer { get; set; }

        public static Settings_HttpAddress ByDefault() => new Settings_HttpAddress();

        private Settings_HttpAddress()
        {            
            SkelyaServer = "https://service.address.com:80/";
        }
    }

    public class Settings_FolderPath
    {
        [JsonProperty("RootStorageFolder")]
        public String RootStorageFolder { get; set; }

        [JsonProperty("CommentsFolder")]
        public String CommentsFolder { get; set; }

        [JsonProperty("DataSectorsFolder")]
        public String DataSectorsFolder { get; set; }

        [JsonProperty("DevicesFolder")]
        public String DevicesFolder { get; set; }

        [JsonProperty("DeviceTypesFolder")]
        public String DeviceTypesFolder { get; set; }

        [JsonProperty("EventsFolder")]
        public String EventsFolder { get; set; }

        [JsonProperty("EventLogRecordsFolder")]
        public String EventLogRecordsFolder { get; set; }

        [JsonProperty("RolesFolder")]
        public String RolesFolder { get; set; }

        [JsonProperty("SnapshotsFolder")]
        public String SnapshotsFolder { get; set; }

        [JsonProperty("TriggersFolder")]
        public String TriggersFolder { get; set; }

        [JsonProperty("UsersFolder")]
        public String UsersFolder { get; set; }
        
        public static Settings_FolderPath ByDefault() => new Settings_FolderPath();

        private Settings_FolderPath()
        {
            RootStorageFolder = @$"C:\_AppData\{AppSettings.ApplicationId}\Data\";
            CommentsFolder = "Comments";
            DataSectorsFolder = "DataSectors";
            DevicesFolder = "Devices";
            DeviceTypesFolder = "DeviceTypes";
            EventsFolder = "Events";
            EventLogRecordsFolder = "EventLogRecords";
            RolesFolder = "Roles";
            SnapshotsFolder = "Snapshots";
            TriggersFolder = "Triggers";
            UsersFolder = "Users";
        }
    }
}
