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

namespace Freedom.UICore.ViewModels.ProductViews
{
    public class SourceProductViewModel : BaseViewModel
    {
        private readonly string _controller = "/SourceProducts";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly ISourceProductService _sourceProductService;
        private readonly IValidator<ISourceProduct> _validator;

        public SourceProductViewModel(ISourceProductService sourceProductService,
                                      IValidator<ISourceProduct> validator)
        {
            PageTitle = new PageTitle(Lang.SourceProduct, MaterialDesignIcons.AirplaneLanding);
            _sourceProductService = sourceProductService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SourceProduct = new SourceProductBind();
        }

        #region Properties

        private SourceProductBind _sourceProductBind;

        public SourceProductBind SourceProduct { get => _sourceProductBind; set => SetProperty(ref _sourceProductBind, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            SourceProduct.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<SourceProductINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<SourceProductINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<SourceProductINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        SourceProduct.Name = model.Name;
                        SourceProduct.Id = model.Id;
                        SourceProduct.CompanyId = _variableService.CompanyRTO.Id;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<SourceProductINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<SourceProductSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private void NavigationResult()
        {
            EnableControl = true;
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
            SourceProductDto sourceProductDto = _mapper.Map<SourceProductDto>(SourceProduct);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (sourceProductDto.Id == 0)
                {
                    XResponse resp = await _sourceProductService.PostAsync(_prefix, _version, _controller, _postMethod, sourceProductDto,
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
                    XResponse resp = await _sourceProductService.PutAsync(_prefix, _version, _controller, _putMethod, sourceProductDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                SourceProduct.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(ISourceProduct));
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
            SourceProduct.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(SourceProduct);
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