﻿using Freedom.UICore;
using Freedom.UICore.Views.ShellViews;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Freedom.Desktop
{
     
    sealed partial class App : Application
    {


      //  private readonly IConfigurationRoot _configurationRoot;


        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;

            this.RequestedTheme = ApplicationTheme.Light;


            //var builder = new ConfigurationBuilder()
            //                   .SetBasePath(Package.Current.InstalledLocation.Path)
            //                   .AddJsonFile("appsettings.json", optional: false);

            //_configurationRoot = builder.Build();

            ServiceCollection services = new ServiceCollection();
            services.RegisterAutomapper();
            services.RegisterInterfaces();
            services.ConfigureServices();
            services.RegisterSerilog();
            services.RegisterSQLiteRepositories();
            services.RegisterApiServices();
            services.RegisterValidation();
            services.RegisterViewModels();
            services.RegisterReports();

            AppEssential.InitializeServiceProvider(services.BuildServiceProvider());

        }

        
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(ShellPage), e.Arguments);
                }

                // Ensure the current window is active
                Window.Current.Activate();

                
            }
        }

        
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

       
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }



    }
}
