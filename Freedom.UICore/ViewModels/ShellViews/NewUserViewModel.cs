using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class NewUserViewModel : BaseViewModel
    {
        private readonly IShellNavigationService _shellNavigationService;

        public NewUserViewModel(IShellNavigationService shellNavigationService)
        {
            _shellNavigationService = shellNavigationService;
        }

        #region Commands

        public ICommand RegisterUserCommand { get; private set; }

        public override void GoBackCommandExecute()
        {
            base.GoBackCommandExecute();
            _shellNavigationService.GoBack();
        }

        #endregion Commands
    }
}