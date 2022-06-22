using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Freedom.Core.ApiServiceInterface;
using Freedom.Frontend.FontIcons;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.Frontend.RequestBind;
using Freedom.Report.ReportInterface;
//using Freedom.Report.ReportInterface;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.UICore.SendingMessages;
using Freedom.UICore.Views.ReportViews;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Enums;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.ReportModels;
using Freedom.Utility.Request.BankRequest;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ReportViews
{
    public class BankDepositReportViewModel : BaseViewModel
    {
        private readonly string _controller = "/BankDeposit";
        private readonly string _method = "/filterA";
        private readonly string _operationTypeToken;
        private readonly string _bankAccountToken;
        private readonly IBankDepositService _bankDepositService;
        private readonly IValidator<BankDepositRequestBind> _validator;
        private readonly IBankDepositReport _bankDepositReport;
        private BankDepositRequestBind _bankDepositRequest;
        private ObservableCollection<BankDepositINFO> _bankDepositCollection;

        public BankDepositReportViewModel(IBankDepositService bankDepositService,
                                          IValidator<BankDepositRequestBind> validator,
                                          IBankDepositReport bankDepositReport)
        {
            PageTitle = new PageTitle(Lang.BankDepositReport, MaterialDesignIcons.LayersTripleOutline);
            _bankDepositService = bankDepositService;
            _validator = validator;
            _bankDepositReport = bankDepositReport;
            _operationTypeToken = Guid.NewGuid().ToString();
            _bankAccountToken = Guid.NewGuid().ToString();
            SearchOperationTypeCommand = new RelayCommand(SearchOperationType);
            SearchBankAccountCommand = new RelayCommand(SearchBankAccount);
            BankDepositCollection = new ObservableCollection<BankDepositINFO>();
            BankDepositRequest = new BankDepositRequestBind()
            {
                SinceDate = DateTime.UtcNow.AddDays(-8).ToShortDateString(),
                UntilDate = DateTime.UtcNow.ToShortDateString(),
                OffSet = "0",
                Limit = "100"
            };

            CalculateCommand = new RelayCommand(Calculate);
            PrintCommand = new RelayCommand(Print);
            ExportCommand = new RelayCommand(ExportReport);
        }

        #region Properties

        public BankDepositRequestBind BankDepositRequest { get => _bankDepositRequest; set => SetProperty(ref _bankDepositRequest, value); }

        public ObservableCollection<BankDepositINFO> BankDepositCollection
        {
            get => _bankDepositCollection;
            set => SetProperty(ref _bankDepositCollection, value);
        }

        #endregion Properties

        #region Commands

        public ICommand PrintCommand { get; private set; }

        public ICommand ExportCommand { get; private set; }

        public ICommand CalculateCommand { get; private set; }

        public ICommand SearchOperationTypeCommand { get; private set; }

        public ICommand SearchBankAccountCommand { get; private set; }

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
                    OperationTypeINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankDepositRequest.OperationTypeName = model.Name;
                        BankDepositRequest.OperationTypeId = model.Id;
                        _focusService.SetTextboxFocus("txtDate");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<OperationTypeINFO>, string>(this, _operationTypeToken);
                });

            int[] subClassId = new int[] { (int)OperationTypeClass.BankCredit };

            bool respzz = _navigationService.NavigateTo<OperationTypeSearchPage>(new OperationTypeParameter(1, subClassId, _operationTypeToken));
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
                    BankAccountINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        BankDepositRequest.BankAccountId = model.Id;
                        BankDepositRequest.BankName = model.Bank;
                        BankDepositRequest.AccountHolder = model.AccountHolder;
                        BankDepositRequest.MoneyName = model.Money;
                        BankDepositRequest.BankAccountNumber = model.AccountNum;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BankAccountINFO>, string>(this, _bankAccountToken);
                });
            _navigationService.NavigateTo<BankAccountSearchPage>(new NavigationParameter(1, _bankAccountToken.ToString()));
        }

        private void ExportReport()
        {
             _navigationService.NavigateTo(nameof(UnoWebPage), "");

        }

        private async void Print()
        {
            //_bankDepositReport.PrintBankDepositReport(out string filePath);
            //var bt = _bankDepositReport.CreateReport();
            // _navigationService.NavigateTo(nameof(UnoWebPage), bt);

            ////_messageService.ShowMessage("susccess gen", "");
            ///

            //var uri = "https://www.google.com";
            //var psi = new System.Diagnostics.ProcessStartInfo();
            //psi.UseShellExecute = true;
            //psi.FileName = uri;
            //Process.Start(psi);


        }

        private async void Calculate()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            BankDepositRequest.CompanyId = _variableService.CompanyRTO.Id;

            string validationResult = ValidateBankDepositParameter();
            if (!string.IsNullOrEmpty(validationResult))
            {
                IsBusy = false;
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }

            BankDepositRequest request = new  BankDepositRequest()
            {
                BankAccountId = BankDepositRequest.BankAccountId,
                CompanyId = _variableService.CompanyRTO.Id,
                OffSet = BankDepositRequest.OffSet.ToInteger(),
                Limit = BankDepositRequest.Limit.ToInteger(),
                OperationTypeId = BankDepositRequest.OperationTypeId,
                SinceDate = BankDepositRequest.SinceDate.ToDatetimeFirstOfDay().ToInteger(),
                UntilDate = BankDepositRequest.UntilDate.ToDatetimeEndOfDay().ToInteger()
            };

            try
            {
                _cancelTokenSource = new CancellationTokenSource();
                _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

                EntityResponse<BankDepositRPT> response = await _bankDepositService.GetEntityFullAsync<BankDepositRPT, BankDepositRequest>(
                        _prefix, _version, _controller, _method, request, _cancelTokenSource.Token, _variableService.Token);

                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }

                BankDepositRPT bankDepositRpt = response.Entity;

                List<BankDepositINFO> info = _mapper.Map<List<BankDepositINFO>>(bankDepositRpt.BankDepositRTOs);
                BankDepositCollection.Clear();
                if (info != null)
                {
                    BankDepositCollection.AddRange(info);
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "on Search Bank Deposit");
            }
        }

        #endregion Commands

        #region Methods

        private string ValidateBankDepositParameter()
        {
            ValidationResult result = _validator.Validate(BankDepositRequest);
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