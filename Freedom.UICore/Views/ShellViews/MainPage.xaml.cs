using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using Freedom.UICore.ViewModels.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShellViews
{
    public sealed partial class MainPage : Page, IViewModel<MainPageViewModel>
    {
        public MainPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<MainPageViewModel>();



            var nav = AppEssential.ServiceProvider.GetRequiredService<INavigationService>();
         //   nav.InitializeNavigationContainer(this.ContentFrame);

            var statusNavigateService = AppEssential.ServiceProvider.GetRequiredService<IStatusNavigateService>();
         //   statusNavigateService.InitializeStatusBar(this.StatusBarFrame);

        }

        public MainPageViewModel ViewModel { get; set; }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            ViewModel.AutoSuggestBox_TextChanged(sender, args);
        }
    }
}