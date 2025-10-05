using Desafio_BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio_BackEnd.Infrastructure.Persistence.Configurations;

public class CourierConfiguration
{
    public void Configure(EntityTypeBuilder<Courier> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Cnpj).IsRequired().HasMaxLength(18);
        builder.HasIndex(x => x.Cnpj).IsUnique();

        builder.Property(x => x.CnhNumber).IsRequired();
        builder.HasIndex(x => x.CnhNumber).IsUnique();
    }
}
