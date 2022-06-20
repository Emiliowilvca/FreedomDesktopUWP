using CommunityToolkit.Mvvm.Input;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class RecoverPasswordViewModel : BaseViewModel
    {
        private readonly IShellNavigationService _shellNavigationService;

        public RecoverPasswordViewModel(IShellNavigationService shellNavigationService)
        {
            _shellNavigationService = shellNavigationService;

            RecoverPasswordCommand = new RelayCommand(RecoverPasswordCommandExecute);
        }

        #region Commands

        public ICommand RecoverPasswordCommand { get; private set; }

        private void RecoverPasswordCommandExecute()
        {
        }

        public override void GoBackCommandExecute()
        {
            base.GoBackCommandExecute();
            _shellNavigationService.GoBack();
        }

        #endregion Commands
    }
}