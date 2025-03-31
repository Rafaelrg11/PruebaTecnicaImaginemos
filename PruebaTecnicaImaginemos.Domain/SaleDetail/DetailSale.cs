using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Sale;
using PruebaTecnicaImaginemos.Domain.sale_detail.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.sale_detail;

public sealed class DetailSale : Entity
{
    public DetailSale(Guid idDetail,
        Guid idProduct,
        Guid idSale,
        Amount amount,
        PriceUnit unitPrice,
        Total total)
        : base(idDetail)
    {
        IdProduct = idProduct;
        IdSale = idSale;
        Amount = amount;
        UnitPrice = unitPrice;
        Total = total;
    }

    private DetailSale() { }


    public Guid IdProduct { get; private set; }
    public Guid IdSale { get; private set; }
    public Amount Amount { get; private set; }
    public PriceUnit UnitPrice { get; private set; }
    public Total Total { get; private set; }
    public Sales Sale { get; private set; }
    public Products Products { get; private set; }


    public static DetailSale create(Guid idProduct, Guid IdSale, int amount, int unitPrice)
    {
        var detailSale = new DetailSale(Guid.NewGuid(), idProduct, IdSale, new Amount(amount), new PriceUnit(unitPrice), new Total(amount * unitPrice));

        detailSale.RaiseDomainEvent(new DetailSaleCreateDomainEvent(detailSale.Id));

        return detailSale;
    }

    public void Update(SaleDetailDTO2 detail)
    {
        if (detail != null)
        {
            Amount = new Amount(detail.Amount);
            UnitPrice = new PriceUnit(detail.UnitPrice);
            Total = new Total(detail.Amount * detail.UnitPrice);
            IdProduct = detail.IdProduct;
            IdSale = detail.IdSale;
        }
    }
}
