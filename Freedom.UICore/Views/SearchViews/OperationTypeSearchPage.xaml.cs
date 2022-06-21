using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class OperationTypeSearchPage : Page, IViewModel<OperationTypeSearchViewModel>
    {
        public OperationTypeSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<OperationTypeSearchViewModel>();
            DataContext = ViewModel;
        }

        public OperationTypeSearchViewModel ViewModel { get; set; }
    }
}