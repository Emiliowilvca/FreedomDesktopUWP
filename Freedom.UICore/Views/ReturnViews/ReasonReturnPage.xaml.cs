using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReturnViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReturnViews
{
    public sealed partial class ReasonReturnPage : Page, IViewModel<ReasonReturnViewModel>
    {
        public ReasonReturnPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ReasonReturnViewModel>();
        }

        public ReasonReturnViewModel ViewModel { get; set; }
    }
}