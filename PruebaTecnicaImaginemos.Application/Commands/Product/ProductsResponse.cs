using PruebaTecnicaImaginemos.Domain.DTOs.SaleDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Commands.Product;

public class ProductsResponse
{
    public Guid IdProduct { get; set; }
    public string NameProduct { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
}
