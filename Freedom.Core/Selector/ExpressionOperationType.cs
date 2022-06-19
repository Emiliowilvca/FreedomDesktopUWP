using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.Utility.Models.RTO;
using System;
using System.Linq.Expressions;

namespace Freedom.Core
{
    public class ExpressionOperationType
    {
        public static Expression<Func<OperationTypeRTO, OperationTypeINFO>> SelectOperationTypeInfo = x =>
                                                                                          ConvertToOperationTypeInfo(x);

        public static Func<OperationTypeRTO, OperationTypeINFO> ConvertToOperationTypeInfo = x => new OperationTypeINFO()
        {
            Id = x.Id,
            Name = x.Name,
            SubclassId = x.SubclassId,
            IsSelected = false,
            SubClassName = x.SubClassName
        };

        public static Expression<Func<OperationTypeINFO, OperationTypeBind>> SelectOperationTypeBind = x =>
                                                                                      ConvertToOperationTypeBind(x);

        public static Func<OperationTypeINFO, OperationTypeBind> ConvertToOperationTypeBind = x => new OperationTypeBind()
        {
            Id = x.Id,
            Name = x.Name,
            SubclassId = x.SubclassId,
            CompanyId = 0,
            IsSelected = false
        };
    }
}