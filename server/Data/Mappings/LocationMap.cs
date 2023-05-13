using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class LocationMap : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> entity)
        {
            entity.ToTable("location");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("city");

            entity.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("country");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.Latitude)
                .HasPrecision(10)
                .HasColumnName("latitude");

            entity.Property(e => e.Longitude)
                .HasPrecision(10)
                .HasColumnName("longtitude");

            entity.Property(e => e.PostCode)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("postCode");

            entity.Property(e => e.Prefecture)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("prefecture");

            entity.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("region");

            entity.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("street");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");
        }
    }
}