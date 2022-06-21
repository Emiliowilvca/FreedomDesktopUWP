using CommunityToolkit.Mvvm.Input;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Freedom.UICore.ViewModels.SettingViews
{
    public class SettingViewModel : BaseViewModel
    {
        private string _themeTitle;
        private readonly IThemeSelectorService _themeSelectorService;

        public SettingViewModel(IThemeSelectorService themeSelectorService)
        {
            SetThemeCommand = new RelayCommand<string>(SetThemeCommandExecute);
            _themeSelectorService = themeSelectorService;
        }

        private void SetThemeCommandExecute(string themeName)
        {
            if (Enum.TryParse(themeName, out ElementTheme theme) is true)
            {
                _themeSelectorService.SetTheme(theme);

                switch (theme)
                {
                    case ElementTheme.Default:
                        ThemeTitle = "Theme Light";
                        break;

                    case ElementTheme.Light:
                        ThemeTitle = "Theme Light";
                        break;

                    case ElementTheme.Dark:
                        ThemeTitle = "Theme Dark";
                        break;

                    default:
                        ThemeTitle = "Theme Light";
                        break;
                }
            }
        }

        public string ThemeTitle { get => _themeTitle; set => SetProperty(ref _themeTitle, value); }

        public ICommand SetThemeCommand { get; private set; }
    }
}