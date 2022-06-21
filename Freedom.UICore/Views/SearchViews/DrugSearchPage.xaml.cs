using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class DrugSearchPage : Page, IViewModel<DrugSearchViewModel>
    {
        public DrugSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<DrugSearchViewModel>();
            DataContext = ViewModel;
        }

        public DrugSearchViewModel ViewModel { get; set; }
    }
}