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
using Freedom.UICore.Views.OperationViews;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Enums;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.OperationViews
{
    public class OperationTypeViewModel : BaseViewModel
    {
        private readonly string _controller = "/OperationTypes";
        private readonly string _subClassController = "/OperationSubClass";
        private readonly string _subClassMethod = "/subClass";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IOperationTypeService _operationTypeService;
        private readonly IOperationSubClassService _operationSubClassService;
        private readonly IValidator<IOperationType> _validator;

        public OperationTypeViewModel(IOperationTypeService operationTypeService,
                                      IOperationSubClassService operationSubClassService,
                                      IValidator<IOperationType> validator)
        {
            PageTitle = new PageTitle(Lang.OperationType, MaterialDesignIcons.Gavel);
            _sendMessageToken = Guid.NewGuid().ToString();
            _operationTypeService = operationTypeService;
            _operationSubClassService = operationSubClassService;
            _validator = validator;
            OperationType = new OperationTypeBind();
            SubClassOperationCollection = new ObservableCollection<SubClassOperationINFO>();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            LoadSubClassCollection();
            _reloadService.Subscribe(nameof(OperationTypePage), typeof(IOperationType));
        }

        #region Properties

        private OperationTypeBind _operationType;
        private ObservableCollection<SubClassOperationINFO> _subClassOperationCollection;

        public OperationTypeBind OperationType { get => _operationType; set => SetProperty(ref _operationType, value); }

        public ObservableCollection<SubClassOperationINFO> SubClassOperationCollection
        {
            get => _subClassOperationCollection;
            set => SetProperty(ref _subClassOperationCollection, value);
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
            OperationType.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<OperationTypeINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<OperationTypeINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<OperationTypeINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        OperationType.Name = model.Name;
                        OperationType.Id = model.Id;
                        OperationType.CompanyId = _variableService.CompanyRTO.Id;
                        OperationType.SubclassId = model.SubclassId;
                        OperationType.Intials = model.Intials;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<OperationTypeINFO>, string>(this, _sendMessageToken);
                });

            int[] subClassIds = ((OperationTypeClass[])Enum.GetValues(typeof(OperationTypeClass)))
                                    .Select(x => (int)x).ToArray();
            var respzz = _navigationService.NavigateTo<OperationTypeSearchPage>(new OperationTypeParameter(1, subClassIds, _sendMessageToken));
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
            OperationTypeDto operationTypeDto = _mapper.Map<OperationTypeDto>(OperationType);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (operationTypeDto.Id == 0)
                {
                    XResponse resp = await _operationTypeService.PostAsync(_prefix, _version, _controller, _postMethod, operationTypeDto,
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
                    XResponse resp = await _operationTypeService.PutAsync(_prefix, _version, _controller, _putMethod, operationTypeDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                OperationType.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IOperationType));
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
            OperationType.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(OperationType);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private async void LoadSubClassCollection()
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            CancellationToken token = _cancelTokenSource.Token;
            try
            {
                ListResponse<SubClassOperationRTO> response = await _operationSubClassService.GetListAsync(_prefix, _version, _subClassController, _subClassMethod,
                                                                             "", token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"error on get SubClassOperation, {response.Message}");
                }

                List<SubClassOperationINFO> subs = _mapper.Map<List<SubClassOperationINFO>>(response.Collection);

                SubClassOperationCollection.AddRange(subs);
            }
            catch (OperationCanceledException)
            {
                _messageService.ShowMessage(Lang.CancelByTocken, "OperationCanceledException");
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "LoadSubClassCollection");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        #endregion Methods
    }
}