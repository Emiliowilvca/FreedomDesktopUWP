using Freedom.UICore;
using Freedom.UICore.Interface;
using Freedom.UICore.Views.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.Desktop
{
    public sealed partial class ShellPage : Page
    {
        /* =========================================================================================
                     MAIN CONTAINER WINDOW 
           =========================================================================================
         */

        public ShellPage()
        {
            this.InitializeComponent();

            IShellNavigationService shellNavigationService = AppEssential.ServiceProvider.GetRequiredService<IShellNavigationService>();

            shellNavigationService.Initialize(this.ShellFrame);

            //if(!Islogged)
            //{
            //    shellNavigationService.Navigate<LoginPage>();
            //}
            //else
            //{
            //    shellNavigationService.Navigate<MainPage>();
            //}
            shellNavigationService.Navigate<LoginPage>();
        }
    }
}