using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class EmployeeRatingMap : IEntityTypeConfiguration<EmployeeRating>
    {
        public void Configure(EntityTypeBuilder<EmployeeRating> entity)
        {
           entity.ToTable("employee_rating");

            entity.HasIndex(e => e.EmployeeRequestId, "employeeRequestId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.ContactQualityRate).HasPrecision(10);

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

            entity.Property(e => e.JobQualityRate)
                .HasPrecision(10)
                .HasColumnName("jobQualityRate");

            entity.Property(e => e.Stars)
                .HasPrecision(10)
                .HasColumnName("stars");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.EmployeeRequest)
                .WithMany(p => p.EmployeeRatings)
                .HasForeignKey(d => d.EmployeeRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_rating_ibfk_1");
        }
    }
}