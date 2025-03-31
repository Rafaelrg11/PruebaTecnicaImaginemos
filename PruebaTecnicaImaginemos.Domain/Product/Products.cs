using PruebaTecnicaImaginemos.Domain.Abstractions;
using PruebaTecnicaImaginemos.Domain.DTOs.Product;
using PruebaTecnicaImaginemos.Domain.Product.Events;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Domain.Product;

public sealed class Products : Entity
{
    public Products(Guid guid,
        NameProd name,
        Prices price,
        Description description)
    {
        NameProduct = name;
        Price = price;
        Description = description;
    }
    private Products() { }
    public NameProd NameProduct { get; private set; }
    public Prices Price { get; private set; }
    public Description Description { get; private set; }
    public ICollection<DetailSale> DetailSale { get; private set; } = new List<DetailSale>();

    public static Products create(string nameProduct, int price, string description)
    {
        var product = new Products(Guid.NewGuid(), new NameProd(nameProduct), new Prices(price), new Description(description));

        product.RaiseDomainEvent(new ProductCreateDomainEvent(product.Id));

        return product;
    }

    public void update(ProductDTO2 dto)
    {
        if (dto != null)
        {
            NameProduct = new NameProd(dto.NameProduct);
            Description = new Description(dto.Description);
            Price = new Prices(dto.Price);
        }
    }
}
