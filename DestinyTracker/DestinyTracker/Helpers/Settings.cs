using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace DestinyTracker.Helpers
{
    public class Settings
    {
        private readonly ApplicationDataContainer _localSettings;

        public string ContainerName;

        public Settings(string containerName)
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            ContainerName = containerName;
        }

        public string GetStringSetting(string settingName)
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);

            if (container.Values.ContainsKey(settingName))
            {
                return container.Values[settingName].ToString();
            }

            throw new KeyNotFoundException($"Setting {settingName} cannot be found.");
        }

        public void SetSetting(string settingName, string value)
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);

            if (container.Values.ContainsKey(settingName))
            {
                container.Values[settingName] = value;
            }
            else
            {
                container.Values.Add(settingName, value);
            }
        }

        public void SetSetting(string settingName, bool value)
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);

            if (container.Values.ContainsKey(settingName))
            {
                container.Values[settingName] = value;
            }
            else
            {
                container.Values.Add(settingName, value);
            }
        }

        public bool GetBooleanSetting(string settingName)
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);

            if (container.Values.ContainsKey(settingName))
            {
                return bool.Parse(container.Values[settingName].ToString());
            }

            throw new KeyNotFoundException($"Setting {settingName} cannot be found.");
        }

        public void RemoveKey(string settingName)
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);

            if (container.Values.ContainsKey(settingName))
            {
                container.Values.Remove(settingName);
            }

            throw new KeyNotFoundException($"Setting {settingName} cannot be found. Nothing deleted.");
        }

        public string GetAllSettingsAsString()
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);

            var settingsString = "All Settings:\n{\n";

            foreach (var setting in container.Values)
            {
                settingsString += $"\t{setting.Key} => {container.Values[setting.Key]}";
                if (setting.Value != container.Values.Last().Value)
                {
                    settingsString += ",\n";
                }
            }

            return settingsString + "\n}";
        }

        public void ClearSettings()
        {
            var container = _localSettings.CreateContainer(ContainerName, ApplicationDataCreateDisposition.Always);
            container.Values.Clear();
        }
    }
}
