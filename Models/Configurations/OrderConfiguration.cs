using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BenriShop.Models.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("ORDER");

            builder.HasIndex(e => e.UserName)
                .HasName("ORDER_FK");

            builder.Property(e => e.OrderId)
                .HasColumnName("ORDERID")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Payment).HasColumnName("PAYMENT");

            builder.Property(e => e.UserName)
                .HasColumnName("USERNAME")
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.Account)
                .WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("fk_order_order_account");




        }
    }
}
