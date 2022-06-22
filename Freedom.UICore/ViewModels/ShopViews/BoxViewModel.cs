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
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ShopViews
{
    public class BoxViewModel : BaseViewModel
    {
        private readonly string _controller = "/Boxes";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IBoxService _boxService;
        private readonly IValidator<IBox> _validator;
        private readonly string _shopToken;
        private readonly string _moneyToken;

        public BoxViewModel(IBoxService boxService,
                            IValidator<IBox> validator)
        {
            PageTitle = new PageTitle(Lang.Box, MaterialDesignIcons.CashRegister);
            _boxService = boxService;
            _validator = validator;
            _shopToken = Guid.NewGuid().ToString();
            _moneyToken = Guid.NewGuid().ToString();
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(Create);
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
            SearchShopCommand = new RelayCommand(SearchShop);
            SearchMoneyCommand = new RelayCommand(SearchMoney);
            Box = new BoxBind();

            InvoiceFormat = "000-000-0000000";
            CreditNoteFormat = "000-000-0000000";
            PromissoryFormat = "000-000-0000000";
            ReturnFormat = "000-000-0000000";
            ReceiptFormat = "000-000-0000000";
        }

        #region Properties

        private BoxBind _box;
        private string _shopName;
        private string _moneyName;
        private string _creditNoteLastCreted;
        private string _creditNoteSince;
        private string _creditNoteUntil;
        private string _invoiceLastCreated;
        private string _invoiceSince;
        private string _invoiceUntil;
        private string _promissoryLastCreated;
        private string _promissorySince;
        private string _promissoryUntil;
        private string _receiptLastCreated;
        private string _receiptSince;
        private string _returnLastCreated;
        private string _receiptUntil;
        private string _returnSince;
        private string _returnUntil;
        private string _invoiceFormat;
        private string _promissoryFormat;
        private string _creditNoteFormat;
        private string _returnFormat;
        private string _receiptFormat;

        public BoxBind Box { get => _box; set => SetProperty(ref _box, value); }

        public string ShopName { get => _shopName; set => SetProperty(ref _shopName, value); }

        public string MoneyName { get => _moneyName; set => SetProperty(ref _moneyName, value); }

        public string InvoiceLastCreated { get => _invoiceLastCreated; set => SetProperty(ref _invoiceLastCreated, value); }

        public string InvoiceSince { get => _invoiceSince; set => SetProperty(ref _invoiceSince, value); }

        public string InvoiceUntil { get => _invoiceUntil; set => SetProperty(ref _invoiceUntil, value); }

        public string CreditNoteLastCreted { get => _creditNoteLastCreted; set => SetProperty(ref _creditNoteLastCreted, value); }

        public string CreditNoteSince { get => _creditNoteSince; set => SetProperty(ref _creditNoteSince, value); }

        public string CreditNoteUntil { get => _creditNoteUntil; set => SetProperty(ref _creditNoteUntil, value); }

        public string PromissoryLastCreated { get => _promissoryLastCreated; set => SetProperty(ref _promissoryLastCreated, value); }

        public string PromissorySince { get => _promissorySince; set => SetProperty(ref _promissorySince, value); }

        public string PromissoryUntil { get => _promissoryUntil; set => SetProperty(ref _promissoryUntil, value); }

        public string ReceiptLastCreated { get => _receiptLastCreated; set => SetProperty(ref _receiptLastCreated, value); }

        public string ReceiptSince { get => _receiptSince; set => SetProperty(ref _receiptSince, value); }

        public string ReceiptUntil { get => _receiptUntil; set => SetProperty(ref _receiptUntil, value); }

        public string ReturnLastCreated { get => _returnLastCreated; set => SetProperty(ref _returnLastCreated, value); }

        public string ReturnSince { get => _returnSince; set => SetProperty(ref _returnSince, value); }

        public string ReturnUntil { get => _returnUntil; set => SetProperty(ref _returnUntil, value); }

        public string InvoiceFormat { get => _invoiceFormat; set => SetProperty(ref _invoiceFormat, value); }

        public string CreditNoteFormat { get => _creditNoteFormat; set => SetProperty(ref _creditNoteFormat, value); }

        public string PromissoryFormat { get => _promissoryFormat; set => SetProperty(ref _promissoryFormat, value); }

        public string ReceiptFormat { get => _receiptFormat; set => SetProperty(ref _receiptFormat, value); }

        public string ReturnFormat { get => _returnFormat; set => SetProperty(ref _returnFormat, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchShopCommand { get; private set; }

        public ICommand SearchMoneyCommand { get; private set; }

        private void Create()
        {
            EnableControl = false;
            Box.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void Edit()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BoxINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BoxINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BoxINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetBoxById(model.Id);
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<BoxSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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

            SetFieldsToModel(Box);

            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            BoxDto boxDto = _mapper.Map<BoxDto>(Box);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (boxDto.Id == 0)
                {
                    XResponse resp = await _boxService.PostAsync(_prefix, _version, _controller, _postMethod, boxDto,
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
                    XResponse resp = await _boxService.PutAsync(_prefix, _version, _controller, _putMethod, boxDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Box.ResetEntity();
                ShopName = "";
                MoneyName = "";
                ClearFields();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IBox));
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
            Box.ResetEntity();
            ShopName = "";
            MoneyName = "";
            ClearFields();
        }

        private void SearchShop()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<ShopINFO>, string>(this, _shopToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _shopToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<ShopINFO>, string>(this, _shopToken,
                (r, m) =>
                {
                    ShopINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Box.ShopId = model.Id;
                        ShopName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _shopToken);
                });
            _navigationService.NavigateTo<ShopSearchPage>(new NavigationParameter(1, _shopToken.ToString()));
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
                        Box.MoneyId = model.Id;
                        MoneyName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
                });
            _navigationService.NavigateTo<MoneySearchPage>(new NavigationParameter(1, _moneyToken.ToString()));
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Box);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private void ClearFields()
        {
            InvoiceSince = "";
            InvoiceUntil = "";
            InvoiceLastCreated = "";
            CreditNoteSince = "";
            CreditNoteUntil = "";
            CreditNoteLastCreted = "";
            PromissorySince = "";
            PromissoryUntil = "";
            PromissoryLastCreated = "";
            ReturnSince = "";
            ReturnUntil = "";
            ReturnLastCreated = "";
            ReceiptUntil = "";
            ReceiptSince = "";
            ReceiptLastCreated = "";
        }

        private void SetFieldsToModel(BoxBind box)
        {
            box.InvoiceSince = InvoiceSince.ToLong();
            box.InvoiceUntil = InvoiceUntil.ToLong();
            box.InvoiceLastCreated = InvoiceLastCreated.ToLong();
            box.CreditNoteSince = CreditNoteSince.ToLong();
            box.CreditNoteUntil = CreditNoteUntil.ToLong();
            box.CreditNoteLastCreted = CreditNoteLastCreted.ToLong();
            box.PromissorySince = PromissorySince.ToLong();
            box.PromissoryUntil = PromissoryUntil.ToLong();
            box.PromissoryLastCreated = _promissoryLastCreated.ToLong();
            box.ReturnSince = ReturnSince.ToLong();
            box.ReturnUntil = ReturnUntil.ToLong();
            box.ReturnLastCreated = ReturnLastCreated.ToLong();
            box.ReceiptUntil = ReceiptUntil.ToLong();
            box.ReceiptSince = ReceiptSince.ToLong();
            box.ReceiptLastCreated = ReceiptLastCreated.ToLong();
        }

        private async void GetBoxById(int boxId)
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
                var response = await _boxService.GetBoxDtoFull(_prefix, _version, _controller, "", boxId,
                                                                       _cancelTokenSource.Token, _variableService.Token);
                IsBusy = false;
                if (response.Entity == null) return;
                Box = ExpressionBox.Instance().convertToBoxBind(response.Entity);

                MoneyName = response.Entity?.MoneyName ?? "";
                ShopName = response.Entity?.ShopName ?? "";
                LoadFormat(Box);
                EnableControl = false;
                _focusService.SetTextboxFocus("txtName");
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "Load Box");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private void LoadFormat(BoxBind box)
        {
            InvoiceSince = Box.InvoiceSince.ToString(InvoiceFormat);
            InvoiceUntil = Box.InvoiceUntil.ToString(InvoiceFormat);
            InvoiceLastCreated = Box.InvoiceLastCreated.ToString(InvoiceFormat);
            ReceiptSince = Box.ReceiptSince.ToString(ReceiptFormat);
            ReceiptUntil = Box.ReceiptUntil.ToString(ReceiptFormat);
            ReceiptLastCreated = Box.ReceiptLastCreated.ToString(ReceiptFormat);
            PromissorySince = Box.PromissorySince.ToString(PromissoryFormat);
            PromissoryUntil = Box.PromissoryUntil.ToString(PromissoryFormat);
            PromissoryLastCreated = Box.PromissoryLastCreated.ToString(PromissoryFormat);
            ReturnSince = Box.ReturnSince.ToString(ReturnFormat);
            ReturnUntil = Box.ReturnUntil.ToString(ReturnFormat);
            ReturnLastCreated = Box.ReturnLastCreated.ToString(ReturnFormat);
            CreditNoteSince = Box.CreditNoteSince.ToString(CreditNoteFormat);
            CreditNoteUntil = Box.CreditNoteUntil.ToString(CreditNoteFormat);
            CreditNoteLastCreted = Box.CreditNoteLastCreted.ToString(CreditNoteFormat);
        }

        #endregion Methods
    }
}