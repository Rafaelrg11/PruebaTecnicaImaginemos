using PruebaTecnicaImaginemos.Domain.DTOs.Product;
using PruebaTecnicaImaginemos.Domain.DTOs.Sale;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.Sale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;

public sealed class SaleDetailDTO
{
    public Guid IdSaleDetail { get;  set; }
    public Guid IdProduct { get;  set; }
    public Guid IdSale { get;  set; }
    public int Amount { get;  set; }
    public int UnitPrice { get;  set; }
    public int Total { get;  set; }
    public SaleDTO2 Sale { get;  set; }
    public ProductDTO2 Products { get;  set; }
}
