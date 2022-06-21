using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankReleaseCheckPage : Page, IViewModel<BankReleaseCheckViewModel>
    {
        public BankReleaseCheckPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankReleaseCheckViewModel>();
        }

        public BankReleaseCheckViewModel ViewModel { get; set; }
    }
}