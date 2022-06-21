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
//using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Enums;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.ViewModels.BankViews
{
    public class BankDepositViewModel : BaseViewModel
    {
        private readonly string _controller = "/BankDeposit";
        private readonly string _postMethod = "";
        private readonly IBankDepositService _bankDepositService;
        private readonly IValidator<IBankDeposit> _validator;
        private readonly IValidator<IBankDepositDetail> _validatorDetails;
        private readonly IValidator<BankDepositBind> _depositvalidator;
        private readonly IValidator<BankDepositDetailBind> _depositDetailValidator;
        private ObservableCollectionEx<BankDepositDetailBind> _bankDepositDetailCollection;
        private BankDepositBind _bankDepositBind;
        private readonly string _operationTypeToken;
        private readonly string _bankAccountToken;

        public BankDepositViewModel(IBankDepositService bankDepositService,
                                    IValidator<IBankDeposit> validator,
                                    IValidator<IBankDepositDetail> validatorDetails,
                                    IValidator<BankDepositBind> depositvalidator,
                                    IValidator<BankDepositDetailBind> depositDetailValidator)
        {
            PageTitle = new PageTitle(Lang.BankDeposit, MaterialDesignIcons.BankTransferIn);
            _bankDepositService = bankDepositService;
            _validator = validator;
            _validatorDetails = validatorDetails;
            _depositvalidator = depositvalidator;
            _depositDetailValidator = depositDetailValidator;
            EnableControl = true;
            _operationTypeToken = Guid.NewGuid().ToString();
            _bankAccountToken = Guid.NewGuid().ToString();
            AddRowCommand = new RelayCommand(AddRow);
            CreateCommand = new RelayCommand(Create);
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
            DeleteItemCommand = new RelayCommand<BankDepositDetailBind>(DeleteCheckItem); ;
            SearchBankAccountCommand = new RelayCommand(SearchBankAccount);
            SearchOperationTypeCommand = new RelayCommand(SearchOperationType);
            BankDepositDetailCollection = new ObservableCollectionEx<BankDepositDetailBind>();
            BankDepositBind = new BankDepositBind();
            EnableControl = false;
            BankDepositDetailCollection.CollectionChanged += BankDepositDetailCollection_CollectionChanged;
            BankDepositDetailCollection.ItemChanged += BankDepositDetailCollection_ItemChanged;
        }

        #region Properties

        public BankDepositBind BankDepositBind
        {
            get => _bankDepositBind;
            set => SetProperty(ref _bankDepositBind, value);
        }

        public ObservableCollectionEx<BankDepositDetailBind> BankDepositDetailCollection
        {
            get => _bankDepositDetailCollection;
            set => SetProperty(ref _bankDepositDetailCollection, value);
        }

        #endregion Properties

        #region Commands

        public ICommand AddRowCommand { get; private set; }

        public ICommand SearchOperationTypeCommand { get; private set; }

        public ICommand SearchBankAccountCommand { get; private set; }

        public ICommand CreateCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand DeleteItemCommand { get; private set; }

        private void AddRow()
        {
            BankDepositDetailCollection.Add(new BankDepositDetailBind()
            {
                CheckDate = DateTime.UtcNow.ToShortDateString()
            });
        }

        private void SearchOperationType()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<OperationTypeINFO>, string>(this, _operationTypeToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<OperationTypeINFO>, string>(this, _operationTypeToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<OperationTypeINFO>, string>(this, _operationTypeToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankDepositBind.OperationTypeId = model.Id;
                        BankDepositBind.OperationTypeName = model.Name;
                        _focusService.SetTextboxFocus("txtDate");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<OperationTypeINFO>, string>(this, _operationTypeToken);
                });

            int[] subClassId = new int[] { (int)OperationTypeClass.BankCredit };

           var respzz = _navigationService.NavigateTo<OperationTypeSearchPage>(new OperationTypeParameter(1, subClassId, _operationTypeToken));
        }

        private void SearchBankAccount()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BankAccountINFO>, string>(this, _bankAccountToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountINFO>, string>(this, _bankAccountToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BankAccountINFO>, string>(this, _bankAccountToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankDepositBind.BankAccountId = model.Id;
                        BankDepositBind.BankName = model.Bank;
                        BankDepositBind.AccountHolder = model.AccountHolder;
                        BankDepositBind.MoneyName = model.Money;
                        BankDepositBind.BankAccount = model.AccountNum;
                        BankDepositBind.MoneyId = model.MoneyId;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountINFO>, string>(this, _bankAccountToken);
                });
            _navigationService.NavigateTo<BankAccountSearchPage>(new NavigationParameter(1, _bankAccountToken.ToString()));
        }

        private void Create()
        {
            EnableControl = true;
            BankDepositBind.CompanyId = _variableService.CompanyRTO.Id;
            BankDepositBind.UserId = _variableService.UserRTO.Id;
            BankDepositBind.Status = true;
            BankDepositBind.TransactionDate = DateTime.UtcNow.ToShortDateString();
            BankDepositBind.Concept = Lang.BankDeposit.ToUpper();
            _focusService.SetTextboxFocus("txtDate");
        }

        private async void Cancel()
        {
            var _resp = await _messageService.ShowMessageConfirmation(Lang.IntendsToCancel, Lang.Cancel);
            if (_resp == ContentDialogResult.Primary)
            {
                BankDepositBind.ResetEntity();
                BankDepositDetailCollection.Clear();
                EnableControl = false;
            }
        }

        private async void Save()
        {
            if (!EnableControl)
                return;

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            /* validate entity Bind*/
            var validationResult = ValidateBankDepositBind();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }

            /*Create dto*/
            var bankDepositdto = ExpressionBankDeposit.ConvertToBankDepositDto(BankDepositBind);

            /*Validate dto*/
            var validationResultdto = ValidateBankDeposit(bankDepositdto);
            if (!string.IsNullOrEmpty(validationResultdto))
            {
                _statusNotifyService.ShowWarning(validationResultdto);
                return;
            }

            bankDepositdto.DepositDetails = null;

            /*if list > 0 validate items*/
            if (BankDepositDetailCollection.Count > 0)
            {
                /*Validate items dto*/
                var BDDvalidationResult = ValidateBankDepositDetailBind();
                if (!string.IsNullOrEmpty(BDDvalidationResult))
                {
                    _statusNotifyService.ShowWarning(BDDvalidationResult);
                    return;
                }

                /*Convert to items dtos*/
                var bankDepositDetails = BankDepositDetailCollection.Select(x => ExpressionBankDeposit.ToBankDepositDetailDto(x)).ToList();

                /*Validate items dtos*/
                var validationResultDet = ValidateBankDepositDetailDto(bankDepositDetails);
                if (!string.IsNullOrEmpty(validationResultDet))
                {
                    _statusNotifyService.ShowWarning(validationResultDet);
                    return;
                }

                /*Add items dtos to main table*/
                bankDepositdto.DepositDetails = new List<BankDepositDetailDto>();
                bankDepositDetails.ForEach(x => bankDepositdto.DepositDetails.Add(x));
            }

            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                var resp = await _bankDepositService.PostAsync(_prefix, _version, _controller, _postMethod, bankDepositdto,
                                                                    _cancelTokenSource.Token, _variableService.Token);
                if (!resp.Success)
                {
                    IsBusy = false;
                    _messageService.ShowMessage(resp.ResultMessage, "Error on Insert");
                    return;
                }

                BankDepositBind.ResetEntity();
                BankDepositDetailCollection.Clear();
                IsBusy = false;
                EnableControl = false;

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

        #endregion Commands

        #region Methods

        private string ValidateBankDepositDetailDto(List<BankDepositDetailDto> detailDtos)
        {
            foreach (var item in detailDtos)
            {
                ValidationResult result = _validatorDetails.Validate(item);
                if (!result.IsValid)
                {
                    ValidationFailure msg = result.Errors.FirstOrDefault();
                    return msg.ErrorMessage;
                }
            }
            return "";
        }

        private string ValidateBankDepositDetailBind()
        {
            foreach (var item in BankDepositDetailCollection)
            {
                ValidationResult result = _depositDetailValidator.Validate(item);
                if (!result.IsValid)
                {
                    ValidationFailure msg = result.Errors.FirstOrDefault();
                    return msg.ErrorMessage;
                }
            }
            return "";
        }

        private string ValidateBankDepositBind()
        {
            ValidationResult result = _depositvalidator.Validate(BankDepositBind);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private string ValidateBankDeposit(BankDepositDto depositDto)
        {
            ValidationResult result = _validator.Validate(depositDto);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private void SumCheck()
        {
            decimal totalcheck = 0m;
            foreach (var item in BankDepositDetailCollection)
            {
                var checkAmount = NumericHelper.ToDecimal(item.Amount);
                totalcheck += checkAmount;
            }
            BankDepositBind.TotalCheck = totalcheck;
        }

        private void BankDepositDetailCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    SumCheck();

                    break;
            }
        }

        private void DeleteCheckItem(BankDepositDetailBind obj)
        {
            if (obj != null)
            {
                BankDepositDetailCollection.Remove(obj);
            }
        }

        private void BankDepositDetailCollection_ItemChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BankDepositDetailBind.Amount))
            {
                SumCheck();
            }
        }

        #endregion Methods
    }
}