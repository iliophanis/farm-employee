using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class PackageMap : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> entity)
        {
            entity.ToTable("package");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.Discount)
                .HasPrecision(10)
                .HasColumnName("discount");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.MaxRequests)
                .HasColumnType("int(11)")
                .HasColumnName("maxRequests");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");
        }
    }
}