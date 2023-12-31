using DevStore.Core.DomainObjects;
using DevStore.Customers.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevStore.Customers.API.Data.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(c => c.SocialNumber).IsRequired()
            .HasColumnType("varchar(50)");

        builder.OwnsOne(c => c.Email, tf =>
        {
            tf.Property(c => c.Address)
                .IsRequired()
                .HasColumnName("Email")
                .HasColumnType($"varchar({Email.EnderecoMaxLength})");
        });

        // 1 : 1 => Aluno : Address
        builder.HasOne(c => c.Address)
            .WithOne(c => c.Customer);

        builder.ToTable("Customer");
    }
}