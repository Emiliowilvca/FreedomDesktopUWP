using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Freedom.Core.ApiServiceInterface;
using Freedom.Frontend.FontIcons;
using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.UICore.SendingMessages;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CountryViews
{
    public class TownViewModel : BaseViewModel
    {
        private readonly string _controller = "/Towns";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly ITownService _townService;
        private readonly IValidator<ITown> _validator;
        private readonly string _cityToken;

        public TownViewModel(ITownService townService,
                             IValidator<ITown> validator)
        {
            _townService = townService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            _cityToken = Guid.NewGuid().ToString();
            PageTitle = new PageTitle(Lang.Town, MaterialDesignIcons.HomeCity);
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SearchCityCommand = new RelayCommand(SearchCityCommandExecute);
            Town = new TownBind();
        }

        #region Properties

        private TownBind _town;

        public TownBind Town { get => _town; set => SetProperty(ref _town, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            Town.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<TownINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<TownINFO>, string>(this, _sendMessageToken);
            }

            WeakReferenceMessenger.Default.Register<VMSendMessage<TownINFO>, string>(this, _sendMessageToken, (r, m) =>
            {
                TownINFO model = m.Value.FirstOrDefault();
                if (model != null)
                {
                    Town.Id = model.Id;
                    Town.Name = model.Name;
                    Town.CityName = model.CityName;
                    Town.CityId = model.CityId;
                    Town.CompanyId = _variableService.CompanyRTO.Id;
                    EnableControl = false;
                }
            });
            _navigationService.NavigateTo<TownSearchPage>(new NavigationParameter(1, _sendMessageToken));
        }

        private void SearchCityCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CityINFO>, string>(this, _cityToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
            }

            WeakReferenceMessenger.Default.Register<VMSendMessage<CityINFO>, string>(this, _cityToken, (r, m) =>
            {
                CityINFO city = m.Value.FirstOrDefault();
                if (city != null)
                {
                    Town.CityId = city.Id;
                    Town.CityName = city.Name;
                }
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
            });
            _navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _cityToken));
        }

        private async void SaveCommandExecute()
        {
            if (EnableControl)
                return;

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            TownDto townDto = _mapper.Map<TownDto>(Town);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (townDto.Id == 0)
                {
                    XResponse resp = await _townService.PostAsync(_prefix, _version, _controller, _postMethod, townDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Insert");
                        return;
                    }
                }
                else
                {
                    XResponse resp = await _townService.PutAsync(_prefix, _version, _controller, _putMethod, townDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Town.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(ITown));
            }
            catch (TaskCanceledException Tcex)
            {
                IsBusy = false;
                _statusNotifyService.ShowError(Tcex.Message);
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private void CancelCommandExecute()
        {
            EnableControl = true;
            Town.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Town);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        #endregion Methods
    }
}