using System;
using System.IO;

namespace Freedom.Report
{
    public static class Program
    {
        public static string FilePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Freedom", "Reports");

        public static IServiceProvider ServiceProvider { get; private set; }

        public static void InitializeServiceProvider(IServiceProvider service)
        {
            ServiceProvider = service;
        }
    }
}