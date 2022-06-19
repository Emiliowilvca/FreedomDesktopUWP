using Freedom.Frontend.Models.Bindable;
using Freedom.Utility.Models.FullDto;
using System;

namespace Freedom.Core
{
    public sealed class ExpressionCustomer
    {
        private static ExpressionCustomer _instance;

        public static ExpressionCustomer Instance()
        {
            if (_instance == null)
            {
                _instance = new ExpressionCustomer();
            }
            return _instance;
        }

        public Func<CustomerDtoFull, CustomerBind> ConvertToCompanyBind = x => new CustomerBind()
        {
            Address = x.Address,
            BirthDate = x.BirthDate,
            BranchId = x.BranchId,
            CategoryId = x.CategoryId,
            Children = x.Children,
            CityId = x.CityId,
            CompanyId = x.CompanyId,
            Credit = x.Credit,
            DateAdmin = x.DateAdmin,
            Email = x.Email,
            EmployementDate = x.EmployementDate,
            FullName = x.FullName,
            Gender = x.Gender,
            Goods = x.Goods,
            LendProducts = x.LendProducts,
            Id = x.Id,
            MaxDiscount = x.MaxDiscount,
            Mobile1 = x.Mobile1,
            Mobile2 = x.Mobile2,
            Note = x.Note,
            OwnHouse = x.OwnHouse,
            Partner = x.Partner,
            PartnerCI = x.PartnerCI,
            PriceLevel = x.PriceLevel,
            ResidenceDate = x.ResidenceDate,
            RouteId = x.RouteId,
            Ruc = x.Ruc,
            SendMoneyColletor = x.SendMoneyColletor,
            Telephone = x.Telephone,
            TradeName = x.TradeName,
            Workphone = x.Workphone,
            WorkPlace = x.WorkPlace
        };
    }
}