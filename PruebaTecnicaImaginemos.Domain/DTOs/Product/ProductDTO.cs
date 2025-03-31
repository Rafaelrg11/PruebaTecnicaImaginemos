using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.DTOs.Product;

public sealed class ProductDTO
{
    public Guid IdProduct { get; set; }
    public string NameProduct { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
    public ICollection<SaleDetailDTO> detailSales { get; set; } = new List<SaleDetailDTO>();
}
