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
//using Freedom.UICore.Views.SearchViews;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace Freedom.UICore.ViewModels.BankViews
{
    public class BankViewModel : BaseViewModel
    {
        private readonly string _controller = "/Banks";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IBankService _bankService;
        private readonly IValidator<IBank> _validator;
        private BankBind _bank;
        private string _cityName;
        private string _cityToken;

        public BankViewModel(IBankService bankService,
                             IValidator<IBank> validator)
        {
            PageTitle = new PageTitle(Lang.Bank, MaterialDesignIcons.Bank);
            _bankService = bankService;
            _validator = validator;
            _cityToken = Guid.NewGuid().ToString();
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(Create);
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
            SearchCityCommand = new RelayCommand(SearchCity);
            Bank = new BankBind();
        }

        #region Properties

        public BankBind Bank { get => _bank; set => SetProperty(ref _bank, value); }

        public string CityName { get => _cityName; set => SetProperty(ref _cityName, value); }


        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCityCommand { get; private set; }


        private void Create()
        {
            EnableControl = false;
            Bank.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void Edit()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BankINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BankINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetBankById(model.Id);
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<BankSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private async void Save()
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
            BankDto bankDto = _mapper.Map<BankDto>(Bank);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (bankDto.Id == 0)
                {
                    XResponse resp = await _bankService.PostAsync(_prefix, _version, _controller, _postMethod, bankDto,
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
                    XResponse resp = await _bankService.PutAsync(_prefix, _version, _controller, _putMethod, bankDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Bank.ResetEntity();
                CityName = "";
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IBank));
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

        private void Cancel()
        {
            EnableControl = true;
            Bank.ResetEntity();
            CityName = "";
        }

        private void SearchCity()
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
                        Bank.CityId = model.Id;
                        CityName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
                });
            //_navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _cityToken.ToString()));
        }

        #endregion Commands

        #region Methods


        private async void GetBankById(int id)
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                IsBusy = true;
                var response = await _bankService.GetEntityFullAsync<BankDtoFull>(_prefix, _version, _controller, "", id,
                                                                                      _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                IsBusy = false;
                var bank = response.Entity;
                if (bank == null) return;
                Bank = _mapper.Map<BankBind>(bank);
                CityName = bank.CityName;
                EnableControl = false;
                _focusService.SetTextboxFocus("txtName");
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "Load Item");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Bank);
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