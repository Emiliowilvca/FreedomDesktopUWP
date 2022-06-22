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

namespace Freedom.UICore.ViewModels.MoneyViews
{
    public partial class MoneyViewModel : BaseViewModel
    {
        private readonly string _controller = "/Money";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IMoneyService _moneyService;
        private readonly IValidator<IMoney> _validator;

        public MoneyViewModel(IMoneyService moneyService,
                              IValidator<IMoney> validator)
        {
            PageTitle = new PageTitle(Lang.Money, MaterialDesignIcons.CashMultiple);
            _moneyService = moneyService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            Money = new MoneyBind();
        }

        #region Properties

        private MoneyBind _money;
        private string _decimalPlaces;
        private string _exchange;
        private string _commisionPercent;

        public MoneyBind Money { get => _money; set => SetProperty(ref _money, value); }

        public string DecimalPlaces
        {
            get => _decimalPlaces;
            set
            {
                SetProperty(ref _decimalPlaces, value);
                Money.DecimalPlaces = int.TryParse(_decimalPlaces, out int newValue1) ? newValue1 : 0;
            }
        }

        public string Exchange
        {
            get => _exchange;
            set
            {
                SetProperty(ref _exchange, value);
                Money.Exchange = double.TryParse(_exchange, out double newValue2) ? (decimal)newValue2 : 0;
            }
        }

        public string CommisionPercent
        {
            get => _commisionPercent;
            set
            {
                SetProperty(ref _commisionPercent, value);
                Money.CommisionPercent = double.TryParse(_commisionPercent, out double newValue3) ? (decimal)newValue3 : 0;
            }
        }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            Money.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<MoneyINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<MoneyINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Money.Name = model.Name;
                        Money.Id = model.Id;
                        Money.CompanyId = _variableService.CompanyRTO.Id;
                        Money.Symbol = model.Symbol;
                        Money.MoneyBase = model.MoneyBase;
                        Money.IsoCode = model.IsoCode;
                        DecimalPlaces = model.DecimalPlaces.ToString();
                        Exchange = model.Exchange.ToString();
                        CommisionPercent = model.CommisionPercent.ToString();
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<MoneySearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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
            MoneyDto moneyDto = _mapper.Map<MoneyDto>(Money);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (moneyDto.Id == 0)
                {
                    XResponse resp = await _moneyService.PostAsync(_prefix, _version, _controller, _postMethod, moneyDto,
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
                    XResponse resp = await _moneyService.PutAsync(_prefix, _version, _controller, _putMethod, moneyDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                CommisionPercent = "";
                DecimalPlaces = "";
                Exchange = "";
                Money.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IMoney));
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
            CommisionPercent = "";
            DecimalPlaces = "";
            Exchange = "";
            Money.ResetEntity();
        }

        #endregion Commands

        #region Method

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Money);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        #endregion Method
    }
}