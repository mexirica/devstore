using DevStore.Orders.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Orders.Infra.Mappings;

public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.ProductName)
            .IsRequired()
            .HasColumnType("varchar(250)");

        // 1 : N => Order : Pagamento
        builder.HasOne(c => c.Order)
            .WithMany(c => c.OrderItems);

        builder.ToTable("OrderItems");
    }
}