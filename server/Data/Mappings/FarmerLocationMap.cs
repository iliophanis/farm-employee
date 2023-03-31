using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class FarmerLocationMap : IEntityTypeConfiguration<FarmerLocation>
    {
        public void Configure(EntityTypeBuilder<FarmerLocation> entity)
        {
            entity.ToTable("farmer_location");

            entity.HasIndex(e => e.FarmerId, "farmerId");

            entity.HasIndex(e => e.LocationId, "locationId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.Data)
                .IsRequired()
                .HasColumnName("data");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.FarmerId)
                .HasColumnType("int(11)")
                .HasColumnName("farmerId");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.LocationId)
                .HasColumnType("int(11)")
                .HasColumnName("locationId");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Farmer)
                .WithMany(p => p.FarmerLocations)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmer_location_ibfk_2");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.FarmerLocations)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmer_location_ibfk_1");
        }
    }
}