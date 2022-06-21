using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankPage : Page, IViewModel<BankViewModel>
    {
        public BankPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetService<BankViewModel>();
        }

        public BankViewModel ViewModel { get; set; }
    }
}