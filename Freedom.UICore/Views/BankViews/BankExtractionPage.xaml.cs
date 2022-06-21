using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankExtractionPage : Page, IViewModel<BankExtractionViewModel>
    {
        public BankExtractionPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<BankExtractionViewModel>();
        }

        public BankExtractionViewModel ViewModel { get; set; }
    }
}