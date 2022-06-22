using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CountryViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CountryViews
{
    public sealed partial class StatePage : Page, IViewModel<StateViewModel>
    {
        public StatePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<StateViewModel>();
        }

        public StateViewModel ViewModel { get; set; }
    }
}