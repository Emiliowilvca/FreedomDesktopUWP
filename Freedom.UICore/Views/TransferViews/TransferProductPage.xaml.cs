using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Freedom.UICore.ViewModels.TransferViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.TransferViews
{
    public sealed partial class TransferProductPage : Page, IViewModel<TransferProductViewModel>
    {
        public TransferProductPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<TransferProductViewModel>();
        }

        public TransferProductViewModel ViewModel { get; set; }
    }
}