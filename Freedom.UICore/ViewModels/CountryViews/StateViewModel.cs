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
using Freedom.UICore.Views.CountryViews;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CountryViews
{
    public class StateViewModel : BaseViewModel
    {
        private readonly string _controller = "/States";
        private readonly string _country = "/Countries";
        private readonly string _limit = "/limit";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        private readonly IValidator<IState> _validator;

        public StateViewModel(IStateService stateService,
                              ICountryService countryService,
                              IValidator<IState> validator)
        {
            PageTitle = new PageTitle(Lang.State, MaterialDesignIcons.FileImageMarker);
            _sendMessageToken = Guid.NewGuid().ToString();
            _stateService = stateService;
            _countryService = countryService;
            _validator = validator;
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            State = new StateBind();
            CountryCollection = new ObservableCollection<CountryINFO>();
            LoadCountries();
            _reloadService.Subscribe(nameof(StatePage), typeof(ICountry));
        }

        #region Properties

        private StateBind _state;
        private ObservableCollection<CountryINFO> _countryCollection;

        public StateBind State { get => _state; set => SetProperty(ref _state, value); }

        public ObservableCollection<CountryINFO> CountryCollection { get => _countryCollection; set => SetProperty(ref _countryCollection, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCountyCommand { get; private set; }

        public ICommand ReloadCountryCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            State.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<StateINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<StateINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<StateINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        State.Name = model.Name;
                        State.Id = model.Id;
                        State.CompanyId = _variableService.CompanyRTO.Id;
                        State.CountryId = model.CountryId;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<StateINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<StateSearchPage>(new NavigationParameter(1, _sendMessageToken));
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
            StateDto stateDto = _mapper.Map<StateDto>(State);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (stateDto.Id == 0)
                {
                    XResponse resp = await _stateService.PostAsync(_prefix, _version, _controller, _postMethod, stateDto,
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
                    XResponse resp = await _stateService.PutAsync(_prefix, _version, _controller, _putMethod, stateDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                State.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
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
            State.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(State);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private async void LoadCountries()
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            var internet = _networkService.CheckIfInternet();
            RequestByLimit request = new RequestByLimit() { CompanyId = _variableService.CompanyRTO.Id, OffSet = 0, Limit = 999 };
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;
                ListResponse<CountryRTO> response = await _countryService.GetListAsync(_prefix, _version, _country, _limit,
                                                                   request, _cancelTokenSource.Token, _variableService.Token);

                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                List<CountryINFO> collection = _mapper.Map<List<CountryINFO>>(response.Collection);

                CountryCollection.Clear();

                CountryCollection.AddRange(collection);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Load Countries");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        public override void OnNavigateTo(object parameter)
        {
            base.OnNavigateTo(parameter);

            _reloadService.IsRecharge(nameof(StatePage), typeof(ICountry), LoadCountries, true);
        }

        #endregion Methods
    }
}