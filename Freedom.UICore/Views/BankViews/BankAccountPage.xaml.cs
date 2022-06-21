using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankAccountPage : Page, IViewModel<BankAccountViewModel>
    {
        public BankAccountPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankAccountViewModel>();
        }

        public BankAccountViewModel ViewModel { get; set; }
    }
}