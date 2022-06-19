using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;
using System;
using System.Linq.Expressions;

namespace Freedom.Core
{
    public sealed class ExpressionCompany
    {
        private static ExpressionCompany _instance;

        public static ExpressionCompany Instance()
        {
            if (_instance == null)
            {
                _instance = new ExpressionCompany();
            }
            return _instance;
        }

        public Expression<Func<CompanyRTO, CompanyINFO>> SelectCompanyRto = x => ConvertToCompanyInfo(x);

        private static Func<CompanyRTO, CompanyINFO> ConvertToCompanyInfo = x => new CompanyINFO()
        {
            Id = x.Id,
            Name = x.Name,
            IsSelected = false,
            City = x.City,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber
        };

        public Func<CompanyDto, CompanyBind> ConvertToCompanyBind = x => new CompanyBind()
        {
            Id = x.Id,
            Name = x.Name,
            IsSelected = false,
            City = x.City,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
            CompanyOwner = x.CompanyOwner,
            Country = x.Country,
            FacebookAdress = x.FacebookAdress,
            IsAvailable = x.IsAvailable,
            Registered = x.Registered,
            State = x.State,
            TrialDay = x.TrialDay,
            WhatsappPhoneNumber = x.WhatsappPhoneNumber
        };
    }
}