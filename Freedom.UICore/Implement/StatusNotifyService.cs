using Freedom.UICore.Interface;
using Freedom.UICore.Models;
using Freedom.UICore.Views.StatusBarViews;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Freedom.UICore.Implement
{
    public class StatusNotifyService : IStatusNotifyService
    {
        private readonly IStatusNavigateService _statusNavigateService;

        private readonly int _delayNotify;
        private readonly string _notifyDelayTime = "Statusbar.NotifyDelayTime";
        private readonly string _colorDanger = "ColorDanger";
        private readonly string _colorInfo = "ColorInfo";
        private readonly string _colorSuccess = "ColorSuccess";
        private readonly string _colorWarning = "ColorWarning";
        private readonly string _colorTextInverter = "ColorTextInverter";
        private readonly string _colorTextPrimary = "ColorPrimaryText";
        private bool _isBusy = false;

        public StatusNotifyService(IStatusNavigateService statusNavigateService)
        {
            _statusNavigateService = statusNavigateService;
            _delayNotify = 2000; //(int)Application.Current.Resources[_notifyDelayTime];
        }

        public async void ShowError(string message)
        {
            if (_isBusy == true) return;
            _isBusy = true;
            NotifyModel notifyModel = GetNotify(message, "\uEDAE", _colorTextInverter, _colorDanger);
            _statusNavigateService.NavigateTo<StatusBarNotifyPage>(notifyModel);
            await Task.Delay(_delayNotify);
            _statusNavigateService.TryGoBack();
            _isBusy = false;
        }

        public async void ShowInformation(string message)
        {
            if (_isBusy == true) return;
            _isBusy = true;
            NotifyModel notifyModel = GetNotify(message, "\uE840", _colorTextInverter, _colorInfo);
            _statusNavigateService.NavigateTo<StatusBarNotifyPage>(notifyModel);
            await Task.Delay(_delayNotify);
            _statusNavigateService.TryGoBack();
            _isBusy = false;
        }

        public async void ShowSuccess(string message)
        {
            if (_isBusy == true) return;
            _isBusy = true;
            NotifyModel notifyModel = GetNotify(message, "\uE8E1", _colorTextInverter, _colorSuccess);
            _statusNavigateService.NavigateTo<StatusBarNotifyPage>(notifyModel);
            await Task.Delay(_delayNotify);
            _statusNavigateService.TryGoBack();
            _isBusy = false;
        }

        public async void ShowWarning(string message)
        {
            if (_isBusy == true) return;
            _isBusy = true;
            NotifyModel notifyModel = GetNotify(message, "\uE7E7", _colorTextPrimary, _colorWarning);
            _statusNavigateService.NavigateTo<StatusBarNotifyPage>(notifyModel);
            await Task.Delay(_delayNotify);
            _statusNavigateService.TryGoBack();
            _isBusy = false;
        }

        private NotifyModel GetNotify(string message, string fontIcon, string colorText, string background)
        {
            SolidColorBrush ForegroundBrush = Application.Current.Resources[colorText] as SolidColorBrush;
            SolidColorBrush BackgroundBrush = Application.Current.Resources[background] as SolidColorBrush;
            NotifyModel notify = new NotifyModel()
            {
                FontIconGlyph = fontIcon,
                Message = message,
                NotifyBackground = BackgroundBrush.Color.ToString(),
                NotifyForeground = ForegroundBrush.Color.ToString()
            };
            return notify;
        }
    }
}