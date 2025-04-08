using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Application.Responses;

public sealed class SailDetailResponse2
{
    public Guid IdSaleDetail { get; set; }
    public Guid IdProduct { get; set; }
    public Guid IdSale { get; set; }
    public int Amount { get; set; }
    public int UnitPrice { get; set; }
    public int Total { get; set; }
    public ProductResponse2 Product { get; set; }
    public SaleResponse3 sale { get; set; }
}

public sealed class ProductResponse2
{
    public Guid IdProduct { get; set; }
    public string NameProduct { get; set; }
    public int Price { get; set; }
    public string Description { get; set; }
}

public sealed class SaleResponse3
{
    public Guid IdSale { get; set; }
    public DateTime TimeSale { get; set; }
    public Guid UserId { get; set; }
    public UserResponse2 user { get; set; }
}

public sealed class SaleResponse2
{
    public Guid IdSale { get; set; }
    public DateTime TimeSale { get; set; }
    public Guid UserId { get; set; }
    public UserResponse2 user { get; set; }
}

public sealed class UserResponse2
{
    public string Name { get; set; }
    public string DNI { get; set; }
}