using CommunityToolkit.Mvvm.Input;
using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using Freedom.UICore.Models;
using Freedom.UICore.Views.BankViews;
using Freedom.UICore.Views.MainViews;
using Freedom.UICore.Views.SettingViews;
using Freedom.UICore.Views.StatusBarViews;
using Freedom.Utility;
using Freedom.Utility.Langs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(IShellNavigationService shellNavigationService)
        {
            _shellNavigationService = shellNavigationService;
            LoadMenuCategory();
            MenuAutosuggestCollection = new ObservableCollection<string>();

         

            LogoutCommand = new RelayCommand(LogoutCommandExecute);
        }

        public override void GoBackCommandExecute()
        {
            base.GoBackCommandExecute();
            _shellNavigationService.GoBack();
        }

        #region Properties

        private ObservableCollection<string> _menuAutosuggestCollection;
        private ObservableCollection<MenuCategory> _menuCategoryCollection;
        private bool _backRequestStatus;
        private readonly IShellNavigationService _shellNavigationService;

        public ObservableCollection<MenuCategory> MenuItemCollection { get => _menuCategoryCollection; set => SetProperty(ref _menuCategoryCollection, value); }

        public ObservableCollection<string> MenuAutosuggestCollection { get => _menuAutosuggestCollection; set => SetProperty(ref _menuAutosuggestCollection, value); }

        public bool BackRequestStatus { get => _backRequestStatus; set => SetProperty(ref _backRequestStatus, value); }

        #endregion Properties

        #region Commands

        

        public ICommand FilterMenuCommand { get; private set; }

        public ICommand LogoutCommand { get; private set; }

        public override void NavigationCommandExecute()
        {
            base.NavigationCommandExecute();
            _shellNavigationService.GoBack();
        }

       
        private void LogoutCommandExecute()
        {
            _shellNavigationService.GoBack();
        }

        #endregion Commands

        #region Methods

        private void LoadMenuCategory()
        {
            var FileMenus = new ObservableCollection<MenuCategory>();
            //FileMenus.Add(new MenuCategory(Lang.Apply, MaterialDesignIcons.ApplicationImport, typeof(ApplyPage)));
            //FileMenus.Add(new MenuCategory(Lang.BanknotesDenomination, MaterialDesignIcons.Cash100, typeof(MoneyDenominationPage)));
            //FileMenus.Add(new MenuCategory(Lang.Box, MaterialDesignIcons.CashRegister, typeof(BoxPage)));
            //FileMenus.Add(new MenuCategory(Lang.Branch, MaterialDesignIcons.AccountTieHat, typeof(BranchPage)));
            //FileMenus.Add(new MenuCategory(Lang.Brand, MaterialDesignIcons.RegisteredTrademark, typeof(BrandPage)));
            //FileMenus.Add(new MenuCategory(Lang.Carrier, MaterialDesignIcons.TruckFast, typeof(CarrierPage)));
            //FileMenus.Add(new MenuCategory(Lang.Category, MaterialDesignIcons.AccountGroup, typeof(CategoryPage)));
            //FileMenus.Add(new MenuCategory(Lang.Cities, MaterialDesignIcons.City, typeof(CityPage)));
            //FileMenus.Add(new MenuCategory(Lang.Company, MaterialDesignIcons.OfficeBuildingCogOutline, typeof(CompanyPage)));
            //FileMenus.Add(new MenuCategory(Lang.Countries, MaterialDesignIcons.Flag, typeof(CountryPage)));
            //FileMenus.Add(new MenuCategory(Lang.Customer, MaterialDesignIcons.AccountMultiple, typeof(CustomerPage)));
            //FileMenus.Add(new MenuCategory(Lang.CustomerAccount, MaterialDesignIcons.BadgeAccountHorizontalOutline, typeof(CustomerAccountPage)));
            //FileMenus.Add(new MenuCategory(Lang.Group, MaterialDesignIcons.Group, typeof(GroupPage)));
            //FileMenus.Add(new MenuCategory(Lang.Measure, MaterialDesignIcons.WeightKilogram, typeof(MeasurePage)));
            //FileMenus.Add(new MenuCategory(Lang.Money, MaterialDesignIcons.CashMultiple, typeof(MoneyPage)));
            //FileMenus.Add(new MenuCategory(Lang.OperationType, MaterialDesignIcons.Gavel, typeof(OperationTypePage)));
            //FileMenus.Add(new MenuCategory(Lang.Package, MaterialDesignIcons.BottleSodaClassic, typeof(PackagePage)));
            //FileMenus.Add(new MenuCategory(Lang.PaymentType, MaterialDesignIcons.CreditCardMarkerOutline, typeof(PaymentTypePage)));
            //FileMenus.Add(new MenuCategory(Lang.Priority, MaterialDesignIcons.AlertBoxOutline, typeof(PriorityPage)));
            //FileMenus.Add(new MenuCategory(Lang.Product, MaterialDesignIcons.LightbulbOnOutline, typeof(ProductPage)));
            //FileMenus.Add(new MenuCategory(Lang.Product_Drug, MaterialDesignIcons.Pill, typeof(DrugPage)));
            //FileMenus.Add(new MenuCategory(Lang.Product_WayAdmin, MaterialDesignIcons.Needle, typeof(WayAdminPage)));
            //FileMenus.Add(new MenuCategory(Lang.Provider, MaterialDesignIcons.AccountTie, typeof(ProviderPage)));
            //FileMenus.Add(new MenuCategory(Lang.ProviderType, MaterialDesignIcons.BriefcaseAccount, typeof(ProviderTypePage)));
            //FileMenus.Add(new MenuCategory(Lang.ReturnReason, MaterialDesignIcons.ClockEnd, typeof(ReasonReturnPage)));
            //FileMenus.Add(new MenuCategory(Lang.Route, MaterialDesignIcons.Highway, typeof(RoutePage)));
            //FileMenus.Add(new MenuCategory(Lang.Sector, MaterialDesignIcons.VectorIntersection, typeof(SectorProductPage)));
            //FileMenus.Add(new MenuCategory(Lang.Shop, MaterialDesignIcons.Store, typeof(ShopPage)));
            //FileMenus.Add(new MenuCategory(Lang.SourceProduct, MaterialDesignIcons.AirplaneLanding, typeof(SourceProductPage)));
            //FileMenus.Add(new MenuCategory(Lang.State, MaterialDesignIcons.FileImageMarker, typeof(StatePage)));
            //FileMenus.Add(new MenuCategory(Lang.SubGroup, MaterialDesignIcons.SelectGroup, typeof(SubGroupPage)));
            //FileMenus.Add(new MenuCategory(Lang.Town, MaterialDesignIcons.HomeCity, typeof(TownPage)));
            //FileMenus.Add(new MenuCategory(Lang.TypeOfExpenses, MaterialDesignIcons.CreditCardClockOutline, typeof(ExpenceTypePage)));
            //FileMenus.Add(new MenuCategory(Lang.Vehicle, MaterialDesignIcons.TruckCargoContainer, typeof(VehiclePage)));
            //FileMenus.Add(new MenuCategory(Lang.Zone, MaterialDesignIcons.Lan, typeof(ZonePage)));

            var ProccessMenus = new ObservableCollection<MenuCategory>();
            //ProccessMenus.Add(new MenuCategory(Lang.Consumption, MaterialDesignIcons.FoodForkDrink, typeof(ConsumptionPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.Expence, MaterialDesignIcons.Coffee, typeof(ExpencePage)));
            //ProccessMenus.Add(new MenuCategory(Lang.Inventory, MaterialDesignIcons.PackageVariantClosed, typeof(InventoryPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.PriceTag, MaterialDesignIcons.Tag, typeof(PriceTagPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.ProductPrice, MaterialDesignIcons.CurrencyUsd, typeof(ProductPricePage)));
            //ProccessMenus.Add(new MenuCategory(Lang.Purchase, MaterialDesignIcons.TruckDeliveryOutline, typeof(PurchasePage)));
            //ProccessMenus.Add(new MenuCategory(Lang.PurchaseOrder, MaterialDesignIcons.CartPlus, typeof(PurchaseOrderPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.PurchaseWithHoldindTax, MaterialDesignIcons.ArchiveArrowDownOutline, typeof(PurchaseWithholdingTaxPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.ReturnPurchases, MaterialDesignIcons.BasketRemove, typeof(ReturnPurchasePage)));
            //ProccessMenus.Add(new MenuCategory(Lang.Sales, MaterialDesignIcons.CartVariant, typeof(SalePage)));
            //ProccessMenus.Add(new MenuCategory(Lang.SalesOrder, MaterialDesignIcons.CartArrowDown, typeof(SaleOrderPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.SalesReturn, MaterialDesignIcons.CartRemove, typeof(SaleReturnPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.TransferProduct, MaterialDesignIcons.Transfer, typeof(TransferProductPage)));
            //ProccessMenus.Add(new MenuCategory(Lang.CustomerPayment, MaterialDesignIcons.AccountCash, typeof(CustomerPaymentPage)));

            var AdminMenus = new ObservableCollection<MenuCategory>();
            //AdminMenus.Add(new MenuCategory(Lang.CashBoxClose, MaterialDesignIcons.CashLock, typeof(CashBoxClosePage)));
            //AdminMenus.Add(new MenuCategory(Lang.CashBoxOpening, MaterialDesignIcons.CashPlus, typeof(CashBoxOpeningPage)));
            //AdminMenus.Add(new MenuCategory(Lang.CashboxCount, MaterialDesignIcons.CashRefund, typeof(CashBoxCountPage)));
            //AdminMenus.Add(new MenuCategory(Lang.ShopRule, MaterialDesignIcons.Store, typeof(ShopRulePage)));
            //AdminMenus.Add(new MenuCategory(Lang.UnfulfilledOrders, MaterialDesignIcons.CartMinus, typeof(OrderNotDeliveredPage)));
            //AdminMenus.Add(new MenuCategory(Lang.ProviderPayment, MaterialDesignIcons.CalendarTextOutline, typeof(ProviderPaymentPage)));

            var reportMenus = new ObservableCollection<MenuCategory>();
            //reportMenus.Add(new MenuCategory(Lang.CustomerDebtsReport, MaterialDesignIcons.AccountClockOutline, typeof(CustomerFeesReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.CustomerPaymentReport, MaterialDesignIcons.ChartTimelineVariant, typeof(CustomerPaymentReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.InventoryReport, MaterialDesignIcons.ChartTree, typeof(InventoryReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.Kardex, MaterialDesignIcons.ClipboardTextSearchOutline, typeof(KardexReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.PriceList, MaterialDesignIcons.TextBoxMultipleOutline, typeof(PriceListPage)));
            //reportMenus.Add(new MenuCategory(Lang.ProviderDebtReport, MaterialDesignIcons.CalendarClockOutline, typeof(ProviderFeesReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.ProviderPaymentReport, MaterialDesignIcons.ChartBoxOutline, typeof(ProviderPaymentReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.PurchaseReport, MaterialDesignIcons.ArchiveClock, typeof(PurchaseReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.SalesReport, MaterialDesignIcons.ChartBar, typeof(SaleReportPage)));
            //reportMenus.Add(new MenuCategory(Lang.TransferReport, MaterialDesignIcons.FileSwap, typeof(TransferReportPage)));

            var BankMenus = new ObservableCollection<MenuCategory>();
            BankMenus.Add(new MenuCategory(Lang.AccountStatement, MaterialDesignIcons.ChartBoxOutline, typeof(BankAccountStatementPage)));
            BankMenus.Add(new MenuCategory(Lang.Bank, MaterialDesignIcons.Bank, typeof(BankPage), null));
            BankMenus.Add(new MenuCategory(Lang.BankAccount, MaterialDesignIcons.CardAccountDetails, typeof(BankAccountPage)));
            BankMenus.Add(new MenuCategory(Lang.BankAccountType, MaterialDesignIcons.CommentBookmark, typeof(BankAccountTypePage)));
            BankMenus.Add(new MenuCategory(Lang.BankDeposit, MaterialDesignIcons.BankTransferIn, typeof(BankDepositPage)));
           //BankMenus.Add(new MenuCategory(Lang.BankDepositReport, MaterialDesignIcons.LayersTripleOutline, typeof(BankDepositReportPage)));
            BankMenus.Add(new MenuCategory(Lang.BankExtraction, MaterialDesignIcons.BankTransferOut, typeof(BankExtractionPage)));
            BankMenus.Add(new MenuCategory(Lang.BankExtractionReport, MaterialDesignIcons.ScriptTextPlayOutline, typeof(BankExtractionReportPage)));
            BankMenus.Add(new MenuCategory(Lang.BankExtractionType, MaterialDesignIcons.CommentArrowLeft, typeof(BankExtractionTypePage)));
            BankMenus.Add(new MenuCategory(Lang.BouncedCheck, MaterialDesignIcons.CardBulletedOffOutline, typeof(BankBouncedCheckPage)));
            BankMenus.Add(new MenuCategory(Lang.Cards, MaterialDesignIcons.CreditCard, typeof(BankCardsPage)));
            BankMenus.Add(new MenuCategory(Lang.ReleaseCheck, MaterialDesignIcons.UploadMultiple, typeof(BankReleaseCheckPage)));
            BankMenus.Add(new MenuCategory(Lang.Transfers, MaterialDesignIcons.BankTransfer, typeof(BankTransfersPage)));

            var EmployeeMenus = new ObservableCollection<MenuCategory>();
            //EmployeeMenus.Add(new MenuCategory(Lang.Employee, MaterialDesignIcons.AccountHardHat, typeof(EmployeePage)));
            //EmployeeMenus.Add(new MenuCategory(Lang.Job_Position, MaterialDesignIcons.BadgeAccount, typeof(JobPostPage)));
            //EmployeeMenus.Add(new MenuCategory(Lang.job_Sectors, MaterialDesignIcons.AccountSwitch, typeof(JobSectorPage)));

            var ProductionMenus = new ObservableCollection<MenuCategory>();

            var ServiceMenus = new ObservableCollection<MenuCategory>();

            var CattleRaisingMenus = new ObservableCollection<MenuCategory>();

            var UsersMenus = new ObservableCollection<MenuCategory>();
            // UsersMenus.Add(new MenuCategory(Lang.User, MaterialDesignIcons.AccountCircle, typeof(NewUserPage)));
            //UsersMenus.Add(new MenuCategory(Lang.UserSetting, MaterialDesignIcons.AccountCog, typeof(UserSettingPage)));

            FileMenus = new ObservableCollection<MenuCategory>(FileMenus.OrderBy(x => x.Name));
            ProccessMenus = new ObservableCollection<MenuCategory>(ProccessMenus.OrderBy(x => x.Name));
            AdminMenus = new ObservableCollection<MenuCategory>(AdminMenus.OrderBy(x => x.Name));
            reportMenus = new ObservableCollection<MenuCategory>(reportMenus.OrderBy(x => x.Name));
            BankMenus = new ObservableCollection<MenuCategory>(BankMenus.OrderBy(x => x.Name));
            ProductionMenus = new ObservableCollection<MenuCategory>(ProductionMenus.OrderBy(x => x.Name));
            EmployeeMenus = new ObservableCollection<MenuCategory>(EmployeeMenus.OrderBy(x => x.Name));

            if (MenuItemCollection == null)
                MenuItemCollection = new ObservableCollection<MenuCategory>();
            MenuItemCollection.Add(new MenuCategory(Lang.Home, MaterialDesignIcons.Home, typeof(HomePage)));
            MenuItemCollection.Add(new MenuCategory(Lang.MenuMain_TabFile, MaterialDesignIcons.NoteEditOutline, null, FileMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.MenuMain_TabProcess, MaterialDesignIcons.ClipboardClockOutline, null, ProccessMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.MenuMain_TabAdministration, MaterialDesignIcons.BriefcaseOutline, null, AdminMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.Report, MaterialDesignIcons.ChartBar, null, reportMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.Bank, MaterialDesignIcons.BankOutline, null, BankMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.Production, MaterialDesignIcons.FoodOutline, null, ProductionMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.MenuMain_TabEmployee, MaterialDesignIcons.AccountHardHat, null, EmployeeMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.Service, MaterialDesignIcons.HammerWrench, null, ServiceMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.CattleRaising, MaterialDesignIcons.Horse, null, CattleRaisingMenus));
            MenuItemCollection.Add(new MenuCategory(Lang.Users, MaterialDesignIcons.AccountCircle, null, UsersMenus));
        }

        #endregion Methods

        #region Events

        // call navigation item on click
        public void NavigationMain_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs e)
        {
            var clickedItem = e.InvokedItem;
            var clickedItemContainer = e.InvokedItemContainer;

            if (e.IsSettingsInvoked)
            {
                _navigationService.NavigateTo<SettingPage>("");
            }
            else if (e.InvokedItemContainer != null)
            {
                var itemPageKey = e.InvokedItemContainer.Name;
                if (string.IsNullOrEmpty(itemPageKey))
                    return;
                _navigationService.NavigateTo(itemPageKey, "", e.RecommendedNavigationTransitionInfo);
            }
        }

        // on acept the autosugget text in list
        public void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion == null)
                return;
            MenuCategory item = MenuItemCollection.SelectMany(x => x.ChildrenMenus).ToList()
                .Where(x => x.Name == args.ChosenSuggestion.ToString()).FirstOrDefault();

            if (item == null)
                return;

            _navigationService.NavigateTo(item.KeyName, "");
        }

        //search menuItems
        public void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                MenuAutosuggestCollection.Clear();

                var collection = MenuItemCollection.SelectMany(x => x.ChildrenMenus)
                                                   .Select(x => x.Name)
                                                   .Where(x => x.ToUpper().Contains((sender).Text.ToUpper(), StringComparison.InvariantCultureIgnoreCase))
                                                   .ToList();
                if (!collection.Any())
                {
                    MenuAutosuggestCollection.Add("No Results!!");
                    return;
                }
                MenuAutosuggestCollection.AddRange(collection);
            }
        }

        // on init Navigation (on showPage)
        public void NavigationMain_Loaded(object sender, RoutedEventArgs e)
        {
            _navigationService.OnNavigateAction = (pageTitle, goBackStatus) =>
            {
                BackRequestStatus = goBackStatus;
                PageTitle.SetTitle(pageTitle.Title, pageTitle.Glyph, pageTitle.IsVisible);
            };
            PageTitle.IsVisible = false;
            _navigationService.NavigateTo(nameof(HomePage), "");

            _statusNavigateService.Navigate(nameof(StatusBarInfoPage));

            _statusBarService.DisplayAll(new StatusBarItem(_variableService.UserRTO.Email,
                                                          _variableService.CompanyRTO.Name,
                                                          _variableService.ShopRTO.Name,
                                                          _variableService.BoxRTO.Name,
                                                          _variableService.EmployeeRTO.Name,
                                                          _variableService.MoneyRTO.Name));
        }

        // call button goback in navigation control
        public void NavigationMain_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            _navigationService.TryGoBack();
        }

        #endregion Events
    }
}