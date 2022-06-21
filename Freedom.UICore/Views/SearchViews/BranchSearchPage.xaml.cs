using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class BranchSearchPage : Page, IViewModel<BranchSearchViewModel>
    {
        public BranchSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BranchSearchViewModel>();
            DataContext = ViewModel;
        }

        public BranchSearchViewModel ViewModel { get; set; }
    }
}