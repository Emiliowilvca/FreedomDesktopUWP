using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Models.RTO;
using System;
using System.Linq.Expressions;

namespace Freedom.Core
{
    public class ExpressionProvider
    {
        private static ExpressionProvider _instance;

        public static ExpressionProvider Instance()
        {
            if (_instance == null)
            {
                _instance = new ExpressionProvider();
            }
            return _instance;
        }

        public Expression<Func<ProviderDtoFull, ProviderBind>> SelectProviderfull = x => ConvertToProviderfull(x);

        public static Func<ProviderDtoFull, ProviderBind> ConvertToProviderfull = x => new ProviderBind()
        {
            Id = x.Id,
            Name = x.Name,
            IsSelected = false,
            Address = x.Name,
            Authorization = x.Authorization,
            BankAccountNum = x.BankAccountNum,
            BankName = x.BankName,
            CityId = x.CityId,
            CompanyId = x.CompanyId,
            Contact = x.Contact,
            Email = x.Email,
            Expiration = x.Expiration,
            PaymentTypeId = x.PaymentTypeId,
            Phone = x.Phone,
            ProviderTypeID = x.ProviderTypeID,
            Ruc = x.Ruc,
            Timbrado = x.Timbrado
        };

        public Expression<Func<ProviderRTO, ProviderINFO>> SelectProviderInfo = x => ConvertToProviderInfo(x);

        public static Func<ProviderRTO, ProviderINFO> ConvertToProviderInfo = x => new ProviderINFO()
        {
            Id = x.Id,
            Name = x.Name,
            IsSelected = false,
            Address = x.Name,
            Authorization = x.Authorization,
            BankAccountNum = x.BankAccountNum,
            BankName = x.BankName,
            CityId = x.CityId,
            CityName = x.CityName,
            CompanyId = x.CompanyId,
            Contact = x.Contact,
            Email = x.Email,
            Expiration = x.Expiration,
            PaymentTypeId = x.PaymentTypeId,
            PaymentTypeName = x.PaymentTypeName,
            Phone = x.Phone,
            ProviderTypeID = x.ProviderTypeID,
            ProviderTypeName = x.ProviderTypeName,
            Ruc = x.Ruc,
            Timbrado = x.Timbrado
        };

        public static Func<ProviderRTO, ProviderBind> ConvertToProviderBind = x => new ProviderBind()
        {
            Id = x.Id,
            Name = x.Name,
            IsSelected = false,
            Address = x.Name,
            Authorization = x.Authorization,
            BankAccountNum = x.BankAccountNum,
            BankName = x.BankName,
            CityId = x.CityId,
            CompanyId = x.CompanyId,
            Contact = x.Contact,
            Email = x.Email,
            Expiration = x.Expiration,
            PaymentTypeId = x.PaymentTypeId,
            Phone = x.Phone,
            ProviderTypeID = x.ProviderTypeID,
            Ruc = x.Ruc,
            Timbrado = x.Timbrado
        };
    }
}