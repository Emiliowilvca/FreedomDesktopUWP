
using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Globalization;
using System.Reflection;

namespace Freedom.UICore.Interface
{
    public interface IDeviceInfoService
    {
        string ApplicationName { get; }

        string ApplicationVersion { get; }

        TimeSpan AppUptime { get; }

        float AvailableMemory { get; }

        CultureInfo Culture { get; }

        string DeviceFamily { get; }

        string DeviceManufacturer { get; }

        string DeviceModel { get; }

        DateTime FirstUseTime { get; }

        string FirstVersionInstalled { get; }

        bool IsAppUpdated { get; }

        bool IsFirstUse { get; }

        DateTime LastLaunchTime { get; }

        string LastResetTime { get; }

        long LaunchCount { get; }

        DateTime LaunchTime { get; }

        string OperatingSystem { get; }

        ProcessorArchitecture OperatingSystemArchitecture { get; }

        OSVersion OperatingSystemVersion { get; }

        string PreviousVersionInstalled { get; }

        long TotalLaunchCount { get; }

        string GetUUID();
    }
}