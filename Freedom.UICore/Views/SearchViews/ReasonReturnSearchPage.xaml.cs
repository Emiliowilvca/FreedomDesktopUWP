using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class ReasonReturnSearchPage : Page, IViewModel<ReasonReturnSearchViewModel>
    {
        public ReasonReturnSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ReasonReturnSearchViewModel>();
            DataContext = ViewModel;
        }

        public ReasonReturnSearchViewModel ViewModel { get; set; }
    }
}