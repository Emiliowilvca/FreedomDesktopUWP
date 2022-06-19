using Freedom.Frontend.Models.BindableINFO;
using Freedom.Utility.Models.FullDto;
using System;

namespace Freedom.Core
{
    public class ExpressionBank
    {
        public static Func<BankDtoFull, BankINFO> ConvertToBankInfo = x => new BankINFO()
        {
            Address = x.Address,
            City = x.CityName,
            CityId = x.CityId,
            CompanyId = x.CompanyId,
            Id = x.Id,
            Mail = x.Mail,
            Manager = x.Manager,
            Mobile = x.Mobile,
            Name = x.Manager,
            Phone = x.Phone,
            Web = x.Web
        };
    }
}