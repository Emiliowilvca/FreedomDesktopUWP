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
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Freedom.UICore.ViewModels.CountryViews
{
    public class CityViewModel : BaseViewModel
    {
        private readonly string _state = "/States";
        private readonly string _country = "/Countries";
        private readonly string _countryId = "/countryId";
        private readonly string _limit = "/limit";
        private readonly string _controller = "/Cities";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;
        private readonly IStateService _stateService;
        private readonly IValidator<ICity> _validator;
        private bool _isEditmode = false;
        private string _stateToken;

        public CityViewModel(ICityService cityService,
                             ICountryService countryService,
                             IStateService stateService,
                             IValidator<ICity> validator)
        {
            PageTitle = new PageTitle(Lang.City, MaterialDesignIcons.City);
            _cityService = cityService;
            _countryService = countryService;
            _stateService = stateService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            _stateToken = Guid.NewGuid().ToString();

            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SearchStateCommand = new RelayCommand(SearchState);
            City = new CityBind();
            CountryCollection = new ObservableCollection<CountryINFO>();
            StateCollection = new ObservableCollection<StateINFO>();
            _reloadService.Subscribe(nameof(CityPage), typeof(ICountry));
            LoadCountries();
            City.PropertyChanged += City_PropertyChanged;
        }

       

        #region Properties

        private CityBind _city;
        public CityBind City { get => _city; set => SetProperty(ref _city, value); }

        private ObservableCollection<CountryINFO> _countryCollection;

        public ObservableCollection<CountryINFO> CountryCollection
        {
            get => _countryCollection;
            set => SetProperty(ref _countryCollection, value);
        }

        private ObservableCollection<StateINFO> _stateCollection;

        public ObservableCollection<StateINFO> StateCollection
        {
            get => _stateCollection;
            set => SetProperty(ref _stateCollection, value);
        }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchStateCommand { get; private set; }



        private void CreateCommandExecute()
        {
            EnableControl = false;
            City.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }


        private void SearchState()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<StateINFO>, string>(this, _stateToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<StateINFO>, string>(this, _stateToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<StateINFO>, string>(this, _stateToken,
                (r, m) =>
                {
                    StateINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        City.CountryId = model.CountryId;
                        City.CountryName = model.CountryName;
                        City.StateId = model.Id;
                        City.StateName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<StateINFO>, string>(this, _stateToken);
                });

            _navigationService.NavigateTo<StateSearchPage>(new NavigationParameter(1, _stateToken));
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CityINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CityINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    CityINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        City.CountryId = model.CountryId;
                        City.CountryName = model.CountryName;
                        City.StateId = model.StateId;
                        City.StateName = model.StateName;
                        City.Id = model.Id;
                        City.Name = model.Name;
                        City.CompanyId = _variableService.CompanyRTO?.Id ?? 0;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _sendMessageToken);
                });
            
            _navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _sendMessageToken));
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

            string validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            CityDto cityDto = _mapper.Map<CityDto>(City);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (cityDto.Id == 0)
                {
                    XResponse resp = await _cityService.PostAsync(_prefix, _version, _controller, _postMethod, cityDto,
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
                    XResponse resp = await _cityService.PutAsync(_prefix, _version, _controller, _putMethod, cityDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                City.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(ICity));
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
            City.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(City);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private async Task LoadStates(int countryId)
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            RequestById request = new RequestById() { CompanyId = _variableService.CompanyRTO.Id, Id = countryId };
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                IsBusy = true;

                var response = await _stateService.GetListAsync(_prefix, _version, _state, _countryId, request,
                                                                                   _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                List<StateINFO> collection = _mapper.Map<List<StateINFO>>(response.Collection);

                StateCollection.Clear();

                StateCollection.AddRange(collection);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Load States");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private async void LoadCountries()
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

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

       
        

        private async void City_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(CityINFO.CountryId):

                    if (City.CountryId > 0  && !_isEditmode)
                    {
                      await LoadStates(City.CountryId);
                    }
                    break;
            }
        }

        #endregion Methods
    }
}