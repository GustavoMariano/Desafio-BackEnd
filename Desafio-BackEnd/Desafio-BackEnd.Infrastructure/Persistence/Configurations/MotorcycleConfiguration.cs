using Desafio_BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio_BackEnd.Infrastructure.Persistence.Configurations;

public class MotorcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
{
    public void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Plate).IsRequired().HasMaxLength(10);
        builder.HasIndex(x => x.Plate).IsUnique();
    }
}
