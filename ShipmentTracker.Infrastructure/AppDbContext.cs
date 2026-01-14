using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShipmentTracker.Domain.Shipments;
using ShipmentTracker.Infrastructure.Authentication;

namespace ShipmentTracker.Infrastructure;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(builder =>
        {
            base.OnModelCreating(modelBuilder);
            
            builder.ToTable("Shipments");

            builder.HasKey(p => p.Id);
            
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.ShipmentType).IsRequired();
            builder.Property(p => p.Origin).IsRequired();
            builder.Property(p => p.Destination).IsRequired();
        });
    }
}