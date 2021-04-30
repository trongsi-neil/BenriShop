using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenriShop.Models;

namespace BenriShop.Models.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => new { e.UserName, e.OrderId });

            builder.ToTable("ORDER");

            builder.HasIndex(e => e.UserName)
                .HasName("HAD_ORDER_FK");

            builder.Property(e => e.UserName)
                .HasColumnName("USER_NAME")
                .HasMaxLength(40)
                .IsUnicode(false);

            builder.Property(e => e.OrderId)
                .HasColumnName("ORDER_ID")
                .HasMaxLength(200)
                .IsUnicode(false);

            builder.Property(e => e.Payment).HasColumnName("PAYMENT");

            builder.Property(e => e.Status).HasColumnName("STATUS");

            builder.HasOne(d => d.UserNameNavigation)
                .WithMany(p => p.Order)
                .HasForeignKey(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_HAD_ORDER_ACCOUNT");


        }
    }
}
