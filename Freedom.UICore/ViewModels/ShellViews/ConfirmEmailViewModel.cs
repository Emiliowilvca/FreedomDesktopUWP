using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class ConfirmEmailViewModel : BaseViewModel
    {
        private readonly IShellNavigationService _shellNavigationService;

        public ConfirmEmailViewModel(IShellNavigationService shellNavigationService)
        {
            _shellNavigationService = shellNavigationService;
        }

        public ICommand ConfirmCommand { get; private set; }

        public override void GoBackCommandExecute()
        {
            base.GoBackCommandExecute();
            _shellNavigationService.GoBack();
        }
    }
}