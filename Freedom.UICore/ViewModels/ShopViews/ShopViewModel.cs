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

namespace Freedom.UICore.ViewModels.ShopViews
{
    public class ShopViewModel : BaseViewModel
    {
        private readonly string _controller = "/Shops";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IShopService _shopService;
        private readonly IValidator<IShop> _validator;
        private readonly string _cityToken;

        public ShopViewModel(IShopService shopService,
                             IValidator<IShop> validator)
        {
            PageTitle = new PageTitle(Lang.Shop, MaterialDesignIcons.Store);
            _shopService = shopService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            _cityToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SearchCityCommand = new RelayCommand(SearchCityCommandExecute);
            Shop = new ShopBind();
        }

        #region Properties

        private ShopBind _shop;
        private string _cityName;
        private string _moneyName;

        public ShopBind Shop { get => _shop; set => SetProperty(ref _shop, value); }

        public string CityName { get => _cityName; set => SetProperty(ref _cityName, value); }

        public string MoneyName { get => _moneyName; set => SetProperty(ref _moneyName, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            Shop.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<ShopINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<ShopINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Shop.Id = model.Id;
                        Shop.Name = model.Name;
                        Shop.Address = model.Address;
                        Shop.BranchManager = model.BranchManager;
                        Shop.CityId = model.cityId;
                        Shop.CompanyId = model.CompanyId;
                        Shop.Mail = model.Mail;
                        Shop.Phone = model.Phone;
                        CityName = model.CityName;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<ShopSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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
            ShopDto shopDto = _mapper.Map<ShopDto>(Shop);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (shopDto.Id == 0)
                {
                    XResponse resp = await _shopService.PostAsync(_prefix, _version, _controller, _postMethod, shopDto,
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
                    XResponse resp = await _shopService.PutAsync(_prefix, _version, _controller, _putMethod, shopDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Shop.ResetEntity();
                CityName = "";
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IApply));
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
            Shop.ResetEntity();
            CityName = "";
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
                        Shop.CityId = model.Id;
                        CityName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
                });
            _navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _cityToken.ToString()));
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Shop);
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