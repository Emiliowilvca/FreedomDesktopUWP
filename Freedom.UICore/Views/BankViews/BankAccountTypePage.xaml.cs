using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankAccountTypePage : Page, IViewModel<BankAccountTypeViewModel>
    {
        public BankAccountTypePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<BankAccountTypeViewModel>();
        }

        public BankAccountTypeViewModel ViewModel { get; set; }
    }
}