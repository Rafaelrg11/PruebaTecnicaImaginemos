using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using PruebaTecnicaImaginemos.Domain.Sale.Events;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using PruebaTecnicaImaginemos.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Sale;

public sealed class Sales : Entity
{
    public Sales(Guid id,
        Guid userId)
        : base(id)
    {
        UserId = userId;
    }
    private Sales() { }

    public DateTime TimeSale { get; private set; } = DateTime.UtcNow;
    public Guid UserId { get; private set; }
    public UserE User { get; private set; }
    public ICollection<DetailSale> detailSales { get; private set; } = new List<DetailSale>();

    public static Sales Create(Guid userId)
    {
        var sale = new Sales(Guid.NewGuid(), userId);
        sale.RaiseDomainEvent(new SaleCreateDomainEvent(sale.Id));
        return sale;
    }

    public void Update(SaleDTO2 sale)
    {
        if (sale is not null)
        {
            UserId = sale.UserId;
        }
    }
}
