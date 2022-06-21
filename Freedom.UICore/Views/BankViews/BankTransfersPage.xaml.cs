using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankTransfersPage : Page, IViewModel<BankTransfersViewModel>
    {
        public BankTransfersPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<BankTransfersViewModel>();
        }

        public BankTransfersViewModel ViewModel { get; set; }
    }
}