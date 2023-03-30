using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class FarmerRatingMap : IEntityTypeConfiguration<FarmerRating>
    {
        public void Configure(EntityTypeBuilder<FarmerRating> entity)
        {
            entity.ToTable("farmer_rating");

            entity.HasIndex(e => e.EmployeeRequestId, "employeeRequestId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.EmployeeRequestId)
                .HasColumnType("int(11)")
                .HasColumnName("employeeRequestId");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.PaymentConsequence)
                .HasPrecision(10)
                .HasColumnName("paymentConsequence");

            entity.Property(e => e.Stars)
                .HasPrecision(10)
                .HasColumnName("stars");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.WorkPlaceRate)
                .HasPrecision(10)
                .HasColumnName("workPlaceRate");

            entity.HasOne(d => d.EmployeeRequest)
                .WithMany(p => p.FarmerRatings)
                .HasForeignKey(d => d.EmployeeRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmerrating_ibfk_1");
        }
    }
}