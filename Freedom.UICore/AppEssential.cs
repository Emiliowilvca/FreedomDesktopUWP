using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.UI.Xaml;

namespace Freedom.UICore
{
    public static class AppEssential
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IConfigurationRoot Configuration { get; set; }

        public static Window MainWindow { get; set; }

        public static void InitializeServiceProvider(ServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Freedom.Report.Program.InitializeServiceProvider(serviceProvider);
        }
    }
}