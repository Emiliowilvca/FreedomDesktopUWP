using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.BankViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.BankViews
{
    public sealed partial class BankCardsPage : Page, IViewModel<BankCardsViewModel>
    {
        public BankCardsPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BankCardsViewModel>();
        }

        public BankCardsViewModel ViewModel { get; set; }
    }
}