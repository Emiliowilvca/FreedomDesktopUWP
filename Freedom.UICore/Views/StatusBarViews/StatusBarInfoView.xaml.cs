using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Freedom.UICore.ViewModels.StatusBarViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Freedom.UICore.Views.StatusBarViews
{
    public sealed partial class StatusBarInfoView : UserControl, IViewModel<StatusBarInfoViewModel>
    {
        public StatusBarInfoViewModel ViewModel { get; set; }

        public StatusBarInfoView()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<StatusBarInfoViewModel>();
        }
    }
}