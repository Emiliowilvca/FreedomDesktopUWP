
using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class TownSearchPage : Page, IViewModel<TownSearchViewModel>
    {
        public TownSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<TownSearchViewModel>();
            DataContext = ViewModel;
        }

        public TownSearchViewModel ViewModel { get; set; }
    }
}