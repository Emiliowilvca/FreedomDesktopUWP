using CommunityToolkit.Mvvm.Input;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.SqliteModels;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using System;
using System.Linq;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class UrlBaseViewModel : BaseViewModel
    {
        private string _endPoint;
        private readonly IApiUrlBaseRepository _apiUrlBaseRepository;
        private readonly IShellNavigationService _shellNavigationService;

        public UrlBaseViewModel(IApiUrlBaseRepository apiUrlBaseRepository,
                                IShellNavigationService shellNavigationService)
        {
            _apiUrlBaseRepository = apiUrlBaseRepository;
            _shellNavigationService = shellNavigationService;
            SaveCommand = new RelayCommand(SaveCommandExecute);
            LoadIfExist();
        }

        private void LoadIfExist()
        {
            ApiUrlBase urlbase = _apiUrlBaseRepository.GetAll().FirstOrDefault();

            if (urlbase != null)
            {
                EndPoint = urlbase.EndPoint;
            }
        }

        private void SaveCommandExecute()
        {
            if (string.IsNullOrEmpty(EndPoint))
                return;

            ApiUrlBase urlbase = _apiUrlBaseRepository.GetAll().FirstOrDefault();

            if (urlbase != null)
            {
                urlbase.EndPoint = EndPoint;
                _apiUrlBaseRepository.Update(urlbase);
            }
            else
            {
                ApiUrlBase apiUrlBase = new ApiUrlBase()
                {
                    EndPoint = EndPoint,
                    EndPointAccount = "",
                    Id = Guid.NewGuid()
                };
                _apiUrlBaseRepository.Insert(apiUrlBase);
            }
            _shellNavigationService.GoBack();
        }

        public string EndPoint { get => _endPoint; set => SetProperty(ref _endPoint, value); }

        public ICommand SaveCommand { get; private set; }

        public override void GoBackCommandExecute()
        {
            base.GoBackCommandExecute();
            _shellNavigationService.GoBack();
        }
    }
}