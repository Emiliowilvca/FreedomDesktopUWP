using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Freedom.Core;
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
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ProviderViews
{
    public partial class ProviderViewModel : BaseViewModel
    {
        private readonly string _controller = "/Providers";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IValidator<IProvider> _validator;
        private readonly ILogger<ProviderViewModel> _logger;
        private readonly IProviderService _providerService;
        private ProviderBind _provider;
        private string _cityName;
        private string _providerType;
        private string _paymentType;
        private readonly string _cityToken;
        private readonly string _providerTypeToken;
        private readonly string _paymentTypeToken;

        public ProviderViewModel(IProviderService providerService,
                                IValidator<IProvider> validator,
                                ILogger<ProviderViewModel> logger)
        {
            PageTitle = new PageTitle(Lang.Provider, MaterialDesignIcons.AccountTie);
            _providerService = providerService;
            _validator = validator;
            _logger = logger;
            _cityToken = Guid.NewGuid().ToString();
            _providerTypeToken = Guid.NewGuid().ToString();
            _paymentTypeToken = Guid.NewGuid().ToString();
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SearchCityCommand = new RelayCommand(SearchCityCommandExecute);
            SearchPaymentTypeCommand = new RelayCommand(SearchPaymentTypeCommandExecute);
            SearchProviderTypeCommand = new RelayCommand(SearchProviderTypeCommandExecute);
            Provider = new ProviderBind() { Authorization = DateTime.UtcNow, Expiration = DateTime.UtcNow.AddDays(30) };
        }

        #region Properties

        public ProviderBind Provider { get => _provider; set => SetProperty(ref _provider, value); }

        public string CityName { get => _cityName; set => SetProperty(ref _cityName, value); }

        public string ProviderType { get => _providerType; set => SetProperty(ref _providerType, value); }

        public string PaymentType { get => _paymentType; set => SetProperty(ref _paymentType, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        public ICommand SearchProviderTypeCommand { get; private set; }

        public ICommand SearchPaymentTypeCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;

            Provider.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<ProviderINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<ProviderINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<ProviderINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    ProviderINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetProviderById(model.Id);
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ProviderINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<ProviderSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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
            ProviderDto providerDto = _mapper.Map<ProviderDto>(Provider);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (Provider.Id == 0)
                {
                    XResponse resp = await _providerService.PostAsync(_prefix, _version, _controller, _postMethod, providerDto,
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
                    XResponse resp = await _providerService.PutAsync(_prefix, _version, _controller, _putMethod, providerDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Provider.ResetEntity();
                CityName = "";
                PaymentType = "";
                ProviderType = "";
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IApply));
            }
            catch (TaskCanceledException Tcex)
            {
                IsBusy = false;
                _statusNotifyService.ShowError(Tcex.Message);
                string exMessage = Tcex?.Message ?? "";
                _logger.LogError(Tcex, "TaskCanceledException {0}", Tcex.Message);
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private void CancelCommandExecute()
        {
            Provider.ResetEntity();
            CityName = "";
            PaymentType = "";
            ProviderType = "";
            EnableControl = true;
        }

        private void SearchCityCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CityINFO>, string>(this, _cityToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CityINFO>, string>(this, _cityToken,
                (r, m) =>
                {
                    CityINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Provider.CityId = model.Id;
                        CityName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
                });
            _navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _cityToken.ToString()));
        }

        private void SearchProviderTypeCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<ProviderTypeINFO>, string>(this, _providerTypeToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<ProviderTypeINFO>, string>(this, _providerTypeToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<ProviderTypeINFO>, string>(this, _providerTypeToken,
                (r, m) =>
                {
                    ProviderTypeINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Provider.ProviderTypeID = model.Id;
                        ProviderType = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ProviderTypeINFO>, string>(this, _providerTypeToken);
                });
            _navigationService.NavigateTo<ProviderTypeSearchPage>(new NavigationParameter(1, _providerTypeToken.ToString()));
        }

        private void SearchPaymentTypeCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<PaymentTypeINFO>, string>(this, _paymentTypeToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<PaymentTypeINFO>, string>(this, _paymentTypeToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<PaymentTypeINFO>, string>(this, _paymentTypeToken,
                (r, m) =>
                {
                    PaymentTypeINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Provider.PaymentTypeId = model.Id;
                        PaymentType = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<PaymentTypeINFO>, string>(this, _paymentTypeToken);
                });
            _navigationService.NavigateTo<PaymentTypeSearchPage>(new NavigationParameter(1, _paymentTypeToken.ToString()));
        }

        #endregion Commands

        #region Methods

        private async void GetProviderById(int providerId)
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            RequestById request = new RequestById() { CompanyId = _variableService.CompanyRTO.Id, Id = providerId };
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                var response = await _providerService.GetEntityFullAsync<ProviderDtoFull>(_prefix, _version, _controller, "", providerId,
                                                                        _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }

                IsBusy = false;
                ProviderDtoFull proInfo = response.Entity;
                if (proInfo == null) return;

                Provider = ExpressionProvider.ConvertToProviderfull(proInfo);

                CityName = proInfo.CityName;
                PaymentType = proInfo.PaymentTypeName;
                ProviderType = proInfo.ProviderTypeName;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "Load Privider");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Provider);
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