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
            nav.InitializeNavigationContainer(this.ContentFrame);

            var statusNavigateService = AppEssential.ServiceProvider.GetRequiredService<IStatusNavigateService>();
             statusNavigateService.InitializeStatusBar(this.StatusBarFrame);

            SuggestTextbox.TextChanged += ViewModel.AutoSuggestBox_TextChanged;
            SuggestTextbox.QuerySubmitted += ViewModel.AutoSuggestBox_QuerySubmitted;
            NavigationMain.ItemInvoked += ViewModel.NavigationMain_ItemInvoked;
            NavigationMain.Loaded += ViewModel.NavigationMain_Loaded;
            NavigationMain.BackRequested += ViewModel.NavigationMain_BackRequested;
            LogoutButton.Tapped += (s, e) => { ViewModel.LogoutCommand.Execute(""); };
        }

        public MainPageViewModel ViewModel { get; set; }
    }
}