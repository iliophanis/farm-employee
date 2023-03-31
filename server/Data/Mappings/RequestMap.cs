using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class RequestMap : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> entity)
        {
            entity.ToTable("request");

            entity.HasIndex(e => e.CultivationId, "cultivationId");

            entity.HasIndex(e => e.FarmerId, "farmerId");

            entity.HasIndex(e => e.LocationId, "locationId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.CultivationId)
                .HasColumnType("int(11)")
                .HasColumnName("cultivationId");

            entity.Property(e => e.EstimatedDuration)
                .HasColumnType("int(11)")
                .HasColumnName("estimatedDuration");

            entity.Property(e => e.FarmerId)
                .HasColumnType("int(11)")
                .HasColumnName("farmerId");

            entity.Property(e => e.FoodAmount)
                .HasPrecision(10)
                .HasColumnName("foodAmount");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.JobType)
                .HasMaxLength(255)
                .HasColumnName("jobType");

            entity.Property(e => e.LocationId)
                .HasColumnType("int(11)")
                .HasColumnName("locationId");

            entity.Property(e => e.Price)
                .HasPrecision(10)
                .HasColumnName("price");

            entity.Property(e => e.StartJobDate).HasColumnName("startJobDate");

            entity.Property(e => e.StayAmount)
                .HasPrecision(10)
                .HasColumnName("stayAmount");

            entity.Property(e => e.TravelAmount)
                .HasPrecision(10)
                .HasColumnName("travelAmount");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Cultivation)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.CultivationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_ibfk_2");

            entity.HasOne(d => d.Farmer)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.FarmerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_ibfk_3");

            entity.HasOne(d => d.Location)
                .WithMany(p => p.Requests)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_ibfk_1");
        }
    }
}