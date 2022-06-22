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
using Freedom.Utility.Validation;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CountryViews
{
    public class CountryViewModel : BaseViewModel
    {
        private readonly string _controller = "/Countries";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly ICountryService _countryService;
        private readonly IValidator<ICountry> _validator;

        public CountryViewModel(ICountryService countryService,
                                IValidator<ICountry> validator)
        {
            PageTitle = new PageTitle(Lang.Countries, MaterialDesignIcons.Flag);
            _countryService = countryService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            Country = new CountryBind();
        }

        #region Properties

        private CountryBind _country;

        public CountryBind Country { get => _country; set => SetProperty(ref _country, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            Country.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtCountry");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CountryINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CountryINFO>, string>(this, _sendMessageToken);
            }

            WeakReferenceMessenger.Default.Register<VMSendMessage<CountryINFO>, string>(this, _sendMessageToken, (r, m) =>
             {
                 actionuno(r, m);
             });
            _navigationService.NavigateTo<CountrySearchPage>(new NavigationParameter(1, _sendMessageToken));
        }

        private void actionuno(object r, VMSendMessage<CountryINFO> m)
        {
            var model = m.Value.FirstOrDefault();
            if (model != null)
            {
                Country.Name = model.Name;
                Country.Id = model.Id;
                Country.CompanyId = _variableService.CompanyRTO.Id;
                EnableControl = false;
            }
            WeakReferenceMessenger.Default.Unregister<VMSendMessage<CountryINFO>, string>(this, _sendMessageToken);
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
            CountryDto countryDto = _mapper.Map<CountryDto>(Country);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (countryDto.Id == 0)
                {
                    XResponse resp = await _countryService.PostAsync(_prefix, _version, _controller, _postMethod, countryDto,
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
                    XResponse resp = await _countryService.PutAsync(_prefix, _version, _controller, _putMethod, countryDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Country.ResetEntity();
                EnableControl = true;

                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(ICountry));
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
            IsBusy = false;
            Country.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            CountryValidator validator = new CountryValidator();
            ValidationResult result = _validator.Validate(Country);
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