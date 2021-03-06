using Freedom.UICore.Interface;
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace Freedom.UICore.Implement
{
    public class DeviceInfoService : IDeviceInfoService
    {
        // To get application's name:
        public string ApplicationName => SystemInformation.Instance.ApplicationName;

        // To get application's version:
        public string ApplicationVersion => $"{SystemInformation.Instance.ApplicationVersion.Major}.{SystemInformation.Instance.ApplicationVersion.Minor}.{SystemInformation.Instance.ApplicationVersion.Build}.{SystemInformation.Instance.ApplicationVersion.Revision}";

        // To get the most preferred language by the user:
        public CultureInfo Culture => SystemInformation.Instance.Culture;

        // To get operating system
        public string OperatingSystem => SystemInformation.Instance.OperatingSystem;

        // To get used processor architecture
        public ProcessorArchitecture OperatingSystemArchitecture => (ProcessorArchitecture)SystemInformation.Instance.OperatingSystemArchitecture;

        // To get operating system version
        public OSVersion OperatingSystemVersion => SystemInformation.Instance.OperatingSystemVersion;

        // To get device family
        public string DeviceFamily => SystemInformation.Instance.DeviceFamily;

        // To get device model
        public string DeviceModel => SystemInformation.Instance.DeviceModel;

        // To get device manufacturer
        public string DeviceManufacturer => SystemInformation.Instance.DeviceManufacturer;

        // To get available memory in MB
        public float AvailableMemory => SystemInformation.Instance.AvailableMemory;

        // To get if the app is being used for the first time since it was installed.
        public bool IsFirstUse => SystemInformation.Instance.IsFirstRun;

        // To get if the app is being used for the first time since being upgraded from an older version.
        public bool IsAppUpdated => SystemInformation.Instance.IsAppUpdated;

        // To get the first version installed
        public string FirstVersionInstalled => SystemInformation.Instance.FirstVersionInstalled.ToFormattedString();

        // To get the previous version installed
        public string PreviousVersionInstalled => SystemInformation.Instance.PreviousVersionInstalled.ToFormattedString();

        // To get the first time the app was launched
        public DateTime FirstUseTime => SystemInformation.Instance.FirstUseTime;

        // To get the time the app was launched.
        public DateTime LaunchTime => SystemInformation.Instance.LaunchTime;

        // To get the time the app was previously launched, not including this instance.
        public DateTime LastLaunchTime => SystemInformation.Instance.LastLaunchTime;

        // To get the time the launch count was reset, not including this instance
        public string LastResetTime => SystemInformation.Instance.LastResetTime.ToString(Culture.DateTimeFormat);

        // To get the number of times the app has been launched since the last reset.
        public long LaunchCount => SystemInformation.Instance.LaunchCount;

        // To get the number of times the app has been launched.
        public long TotalLaunchCount => SystemInformation.Instance.TotalLaunchCount;

        // To get how long the app has been running
        public TimeSpan AppUptime => SystemInformation.Instance.AppUptime;

        public string GetUUID()
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "CMD.exe";
            startInfo.Arguments = "/C wmic csproduct get UUID";
            process.StartInfo = startInfo;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();
            return output;
        }
    }
}