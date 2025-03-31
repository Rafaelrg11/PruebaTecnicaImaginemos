using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaImaginemos.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Infraestructure.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<UserE>
{
    public void Configure(EntityTypeBuilder<UserE> builder)
    {
        builder.ToTable("users");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(256)
            .HasConversion(e => e.value, value => new Name(value));

        builder.Property(e => e.DNI)
            .HasMaxLength(256)
            .HasConversion(e => e.dni, value => new DNI(value));

        builder.Property<uint>("Version").IsRowVersion();
    }
}
