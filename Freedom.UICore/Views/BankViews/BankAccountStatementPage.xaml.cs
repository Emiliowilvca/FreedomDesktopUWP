using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankAccountStatementPage : Page, IViewModel<BankAccountStatementViewModel>
    {
        public BankAccountStatementPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<BankAccountStatementViewModel>();
        }

        public BankAccountStatementViewModel ViewModel { get; set; }
    }
}