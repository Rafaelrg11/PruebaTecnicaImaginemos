using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaImaginemos.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Configuration;

internal class ProductConfiguration : IEntityTypeConfiguration<Products>
{
    public void Configure(EntityTypeBuilder<Products> builder)
    {
        builder.ToTable("products");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.NameProduct)
            .HasMaxLength(300)
            .HasConversion(e => e.value, value => new NameProd(value));

        builder.Property(e => e.Price)
            .HasConversion(e => e.price, value => new Prices(value));

        builder.Property(e => e.Description)
            .HasMaxLength(300)
            .HasConversion(e => e.value, value => new Description(value));

        builder.Property<uint>("Version").IsRowVersion();
    }
}
