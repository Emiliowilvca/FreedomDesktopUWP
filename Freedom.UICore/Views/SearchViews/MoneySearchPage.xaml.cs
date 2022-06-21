using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class MoneySearchPage : Page, IViewModel<MoneySearchViewModel>
    {
        public MoneySearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<MoneySearchViewModel>();
            DataContext = ViewModel;
        }

        public MoneySearchViewModel ViewModel { get; set; }
    }
}