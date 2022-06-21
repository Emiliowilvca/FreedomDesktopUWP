using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankExtractionTypePage : Page, IViewModel<BankExtractionTypeViewModel>
    {
        public BankExtractionTypePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankExtractionTypeViewModel>();
        }

        public BankExtractionTypeViewModel ViewModel { get; set; }
    }
}