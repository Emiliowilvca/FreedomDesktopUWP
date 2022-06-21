using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankBouncedCheckPage : Page, IViewModel<BankBouncedCheckViewModel>
    {
        public BankBouncedCheckPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankBouncedCheckViewModel>();
        }

        public BankBouncedCheckViewModel ViewModel { get; set; }
    }
}