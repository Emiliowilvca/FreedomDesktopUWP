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
namespace Freedom.UICore.ViewModels.BankViews
{
    public class BankAccountViewModel : BaseViewModel
    {
        private readonly string _controller = "/BankAccounts";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IBankAccountService _bankAccountService;
        private readonly IValidator<IBankAccount> _validator;
        private readonly string _moneyToken;
        private readonly string _bankToken;
        private readonly string _bankAccountTypeToken;

        public BankAccountViewModel(IBankAccountService bankAccountService,
                                    IValidator<IBankAccount> validator)
        {
            PageTitle = new PageTitle(Lang.BankAccount, MaterialDesignIcons.CardAccountDetails);
            _bankAccountService = bankAccountService;
            _moneyToken = Guid.NewGuid().ToString();
            _bankToken = Guid.NewGuid().ToString();
            _bankAccountTypeToken = Guid.NewGuid().ToString();
            _sendMessageToken = Guid.NewGuid().ToString();
            
            _validator = validator;
            EnableControl = true;
            CreateCommand = new RelayCommand(Create);
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
            SearchBankAccountTypeCommand = new RelayCommand(SearchBankAccountType);
            SearchMoneyCommand = new RelayCommand(SearchMoney);
            SearchBankCommand = new RelayCommand(SearchBank);             
            BankAccount = new BankAccountBind();
        }

        #region Properties

        private BankAccountBind _bankAccount;
        private string _moneyName;
        private string _bankName;
        private string _bankAccountTypeName;
        

        public BankAccountBind BankAccount { get => _bankAccount; set => SetProperty(ref _bankAccount, value); }

        public string MoneyName { get => _moneyName; set => SetProperty(ref _moneyName, value); }

        public string BankName { get => _bankName; set => SetProperty(ref _bankName, value); }

        public string BankAccountTypeName { get => _bankAccountTypeName; set => SetProperty(ref _bankAccountTypeName, value); }

       

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchMoneyCommand { get; private set; }

        public ICommand SearchBankCommand { get; private set; }

        public ICommand SearchBankAccountTypeCommand { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        private void Create()
        {
            EnableControl = false;
            BankAccount.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void Edit()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BankAccountINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BankAccountINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetBankAccountById(model.Id);
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountINFO>, string>(this, _sendMessageToken);
                });
            //_navigationService.NavigateTo<BankAccountSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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
            BankAccountDto bankAccountDto = _mapper.Map<BankAccountDto>(BankAccount);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (bankAccountDto.Id == 0)
                {
                    XResponse resp = await _bankAccountService.PostAsync(_prefix, _version, _controller, _postMethod, bankAccountDto,
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
                    XResponse resp = await _bankAccountService.PutAsync(_prefix, _version, _controller, _putMethod, bankAccountDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                BankAccount.ResetEntity();
                MoneyName = "";
                BankName = "";
                BankAccountTypeName = "";
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IBankAccount));
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
            BankAccount.ResetEntity();
            MoneyName = "";
            BankName = "";
            BankAccountTypeName = "";
        }

         

        private void SearchMoney()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<MoneyINFO>, string>(this, _moneyToken,
                (r, m) =>
                {
                    MoneyINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankAccount.MoneyId = model.Id;
                        MoneyName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
                });
           // _navigationService.NavigateTo<MoneySearchPage>(new NavigationParameter(1, _moneyToken.ToString()));
        }

        private void SearchBank()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BankINFO>, string>(this, _bankToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankINFO>, string>(this, _bankToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BankINFO>, string>(this, _bankToken,
                (r, m) =>
                {
                    BankINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankAccount.BankId = model.Id;
                        BankName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankINFO>, string>(this, _bankToken);
                });
          //  _navigationService.NavigateTo<BankSearchPage>(new NavigationParameter(1, _bankToken.ToString()));
        }

        private void SearchBankAccountType()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BankAccountTypeINFO>, string>(this, _bankAccountTypeToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountTypeINFO>, string>(this, _bankAccountTypeToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BankAccountTypeINFO>, string>(this, _bankAccountTypeToken,
                (r, m) =>
                {
                    BankAccountTypeINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankAccount.BankAccountTypeId = model.Id;
                        BankAccountTypeName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountTypeINFO>, string>(this, _bankAccountTypeToken);
                });
           // _navigationService.NavigateTo<BankAccountTypeSearchPage>(new NavigationParameter(1, _bankAccountTypeToken.ToString()));
        }

        #endregion Commands

        #region Methods

        private async void GetBankAccountById(int id)
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
                var response = await _bankAccountService.GetEntityFullAsync<BankAccountDtoFull>(_prefix, _version, _controller, "", id,
                                                                                      _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                IsBusy = false;
                var bankAccount = response.Entity;
                if (bankAccount == null) return;
                BankAccount = _mapper.Map<BankAccountBind>(bankAccount);
                MoneyName = bankAccount.MoneyName;
                BankName = bankAccount.BankName;
                BankAccountTypeName = bankAccount.BankAccountTypeName;
                EnableControl = false;
                _focusService.SetTextboxFocus("txtName");
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
            ValidationResult result = _validator.Validate(BankAccount);
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