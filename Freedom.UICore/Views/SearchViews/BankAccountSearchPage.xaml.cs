using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class BankAccountSearchPage : Page, IViewModel<BankAccountSearchViewModel>
    {
        public BankAccountSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankAccountSearchViewModel>();
            DataContext = ViewModel;
        }

        public BankAccountSearchViewModel ViewModel { get; set; }
    }
}