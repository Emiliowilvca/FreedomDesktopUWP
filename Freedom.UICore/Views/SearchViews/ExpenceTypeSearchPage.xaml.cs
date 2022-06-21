using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class ExpenceTypeSearchPage : Page, IViewModel<ExpenceTypeSearchViewModel>
    {
        public ExpenceTypeSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ExpenceTypeSearchViewModel>();
            DataContext = ViewModel;
        }

        public ExpenceTypeSearchViewModel ViewModel { get; set; }
    }
}