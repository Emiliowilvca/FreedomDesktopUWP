using Freedom.Frontend.Models.Bindable;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using System;

namespace Freedom.Core
{
    public sealed class ExpressionBox
    {
        public static ExpressionBox _instance;

        public static ExpressionBox Instance()
        {
            if (_instance == null)
            {
                _instance = new ExpressionBox();
            }
            return _instance;
        }

        public Func<BoxDtoFull, BoxDto> convertToBoxDto = x => new BoxDto()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            MoneyId = x.MoneyId,
            ShopId = x.ShopId,
            Name = x.Name,
            CreditNoteLastCreted = x.CreditNoteLastCreted,
            CreditNoteSince = x.CreditNoteSince,
            CreditNoteUntil = x.CreditNoteUntil,
            InvoiceLastCreated = x.InvoiceLastCreated,
            InvoiceSince = x.InvoiceSince,
            InvoiceUntil = x.InvoiceUntil,
            PromissoryLastCreated = x.PromissoryLastCreated,
            PromissorySince = x.PromissorySince,
            PromissoryUntil = x.PromissoryUntil,
            ReceiptLastCreated = x.ReceiptLastCreated,
            ReceiptSince = x.ReceiptSince,
            ReceiptUntil = x.ReceiptUntil,
            ReturnLastCreated = x.ReturnLastCreated,
            ReturnSince = x.ReturnSince,
            ReturnUntil = x.ReturnUntil
        };

        public Func<BoxDtoFull, BoxBind> convertToBoxBind = x => new BoxBind()
        {
            Id = x.Id,
            CompanyId = x.CompanyId,
            MoneyId = x.MoneyId,
            ShopId = x.ShopId,
            Name = x.Name,
            CreditNoteLastCreted = x.CreditNoteLastCreted,
            CreditNoteSince = x.CreditNoteSince,
            CreditNoteUntil = x.CreditNoteUntil,
            InvoiceLastCreated = x.InvoiceLastCreated,
            InvoiceSince = x.InvoiceSince,
            InvoiceUntil = x.InvoiceUntil,
            PromissoryLastCreated = x.PromissoryLastCreated,
            PromissorySince = x.PromissorySince,
            PromissoryUntil = x.PromissoryUntil,
            ReceiptLastCreated = x.ReceiptLastCreated,
            ReceiptSince = x.ReceiptSince,
            ReceiptUntil = x.ReceiptUntil,
            ReturnLastCreated = x.ReturnLastCreated,
            ReturnSince = x.ReturnSince,
            ReturnUntil = x.ReturnUntil
        };
    }
}