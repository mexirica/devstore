using DevStore.Orders.Domain.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Orders.Infra.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(c => c.Id);


        builder.Property(c => c.Code)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.ToTable("Vouchers");
    }
}