using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaImaginemos.Domain.Product;
using PruebaTecnicaImaginemos.Domain.sale_detail;
using PruebaTecnicaImaginemos.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Configuration;

internal class SaleDetailConfiguration : IEntityTypeConfiguration<DetailSale>
{

        public void Configure(EntityTypeBuilder<DetailSale> builder)
        {
            builder.ToTable("detail_sale");
            
            builder.HasKey(e => e.Id);

            builder.Property(e => e.UnitPrice)
                .HasConversion(e => e.value, value => new PriceUnit(value));

            builder.Property(e => e.Amount)
                .HasConversion(e => e.amount, value => new Amount(value));

            builder.Property(e => e.Total)
                .HasConversion(e => e.value, value => new Total(value));

            builder.HasOne(e => e.Sale)
                .WithMany(e => e.detailSales)
                .HasForeignKey(e => e.IdSale)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(e => e.Products)
                .WithMany(e => e.DetailSale)
                .HasForeignKey(e => e.IdProduct)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<uint>("Version").IsRowVersion();
        }
}
