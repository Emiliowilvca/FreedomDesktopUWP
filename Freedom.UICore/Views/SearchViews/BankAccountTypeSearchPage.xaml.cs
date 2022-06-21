using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class BankAccountTypeSearchPage : Page, IViewModel<BankAccountTypeSearchViewModel>
    {
        public BankAccountTypeSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankAccountTypeSearchViewModel>();
            DataContext = ViewModel;
        }

        public BankAccountTypeSearchViewModel ViewModel { get; set; }
    }
}