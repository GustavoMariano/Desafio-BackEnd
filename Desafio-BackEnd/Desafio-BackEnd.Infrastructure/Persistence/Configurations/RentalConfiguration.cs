using Desafio_BackEnd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio_BackEnd.Infrastructure.Persistence.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Courier)
               .WithMany(x => x.Rentals)
               .HasForeignKey(x => x.CourierId);

        builder.HasOne(x => x.Motorcycle)
               .WithMany(x => x.Rentals)
               .HasForeignKey(x => x.MotorcycleId);
    }
}
