using CommunityToolkit.Mvvm.Input;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using Freedom.UICore.Views.ShellViews;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IShellNavigationService _shellNavigationService;

        public LoginViewModel(IShellNavigationService shellNavigationService)
        {
            _shellNavigationService = shellNavigationService;
            navigateCommand = new RelayCommand(navigate);
        }

        private void navigate()
        {
            _shellNavigationService.Navigate<MainPage>();
        }

        public ICommand navigateCommand { get; private set; }
    }
}