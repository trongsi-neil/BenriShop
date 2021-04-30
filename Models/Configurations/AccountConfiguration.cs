using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenriShop.Models.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(e => e.UserName);

            builder.ToTable("ACCOUNT");

            builder.Property(e => e.UserName).HasColumnName("USERNAME").HasMaxLength(40).IsUnicode(false);

            builder.Property(e => e.Address).HasColumnName("ADDRESS").HasMaxLength(300);

            builder.Property(e => e.FullName).HasColumnName("FULLNAME").HasMaxLength(100);

            builder.Property(e => e.Password).IsRequired().HasColumnName("PASSWORD").HasMaxLength(40).IsUnicode(false);

            builder.Property(e => e.PhoneNumber).HasColumnName("PHONENUMBER").HasMaxLength(12).IsUnicode(false);

            builder.Property(e => e.Role).IsRequired().HasColumnName("ROLE").HasMaxLength(20).IsUnicode(false);

        }
    }
}
