using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BenriShop.Models.Configurations
{
    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("COLOR");

            builder.Property(e => e.ColorId)
                .HasColumnName("COLORID")
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
