using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.SaleDetail;

public sealed class SaleDetailResponse
{
    public Guid IdSaleDetail { get; set; }
    public Guid IdProduct { get; set; }
    public Guid IdSale { get; set; }
    public int Amount { get; set; }
    public int UnitPrice { get; set; }
    public int Total { get; set; }
}
