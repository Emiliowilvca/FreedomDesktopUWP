using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ExpenceViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ExpenceViews
{
    public sealed partial class ExpenceTypePage : Page, IViewModel<ExpenceTypeViewModel>
    {
        public ExpenceTypePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ExpenceTypeViewModel>();
        }

        public ExpenceTypeViewModel ViewModel { get; set; }
    }
}