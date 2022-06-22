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
using Freedom.UICore.Views.ProductViews;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ProductViews
{
    public class SubGroupViewModel : BaseViewModel
    {
        private readonly string _controller = "/SubGroups";
        private readonly string _group = "/Groups";
        private readonly string _limit = "/limit";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly ISubGroupService _subGroupService;
        private readonly IGroupService _groupService;
        private readonly IValidator<ISubGroup> _validator;

        public SubGroupViewModel(ISubGroupService subGroupService,
                                 IGroupService groupService,
                                 IValidator<ISubGroup> validator)
        {
            PageTitle = new PageTitle(Lang.SubGroup, MaterialDesignIcons.SelectGroup);
            _subGroupService = subGroupService;
            _groupService = groupService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SubGroup = new SubGroupBind();
            GroupCollection = new ObservableCollection<GroupINFO>();
            LoadGroups();
            _reloadService.Subscribe(nameof(SubGroupPage), typeof(IGroup));
        }

        #region Properties

        private SubGroupBind _subGroup;
        private ObservableCollection<GroupINFO> _groupCollection;

        public SubGroupBind SubGroup { get => _subGroup; set => SetProperty(ref _subGroup, value); }

        public ObservableCollection<GroupINFO> GroupCollection { get => _groupCollection; set => SetProperty(ref _groupCollection, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            SubGroup.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<SubGroupINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<SubGroupINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<SubGroupINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        SubGroup.Name = model.Name;
                        SubGroup.Id = model.Id;
                        SubGroup.CompanyId = _variableService.CompanyRTO.Id;
                        SubGroup.GroupId = model.GroupId;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<SubGroupINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<SubGroupSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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
            SubGroupDto subGroupDto = _mapper.Map<SubGroupDto>(SubGroup);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (SubGroup.Id == 0)
                {
                    XResponse resp = await _subGroupService.PostAsync(_prefix, _version, _controller, _postMethod, subGroupDto,
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
                    XResponse resp = await _subGroupService.PutAsync(_prefix, _version, _controller, _putMethod, subGroupDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                SubGroup.ResetEntity();
                EnableControl = true;
                IsBusy = false;
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

        private void CancelCommandExecute()
        {
            EnableControl = true;
            SubGroup.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(SubGroup);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private async void LoadGroups()
        {
            RequestByLimit request = new RequestByLimit() { CompanyId = _variableService.CompanyRTO.Id, OffSet = 0, Limit = 999 };
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;
                ListResponse<GroupRTO> response = await _groupService.GetListAsync(_prefix, _version, _group, _limit,
                                                                   request, _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                List<GroupINFO> collection = _mapper.Map<List<GroupINFO>>(response.Collection);

                GroupCollection.Clear();

                GroupCollection.AddRange(collection);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Load Groups");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        #endregion Methods
    }
}