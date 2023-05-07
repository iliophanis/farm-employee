using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("employee");

            entity.HasIndex(e => e.ContactInfoId, "contactInfoId");

            entity.HasIndex(e => e.DocumentId, "documentId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.AvgContactQuality)
                .HasPrecision(10)
                .HasColumnName("avgContactQuality");

            entity.Property(e => e.AvgJobQuality)
                .HasPrecision(10)
                .HasColumnName("avgJobQuality");

            entity.Property(e => e.AvgPrice)
                .HasPrecision(10)
                .HasColumnName("avgPrice");

            entity.Property(e => e.AvgRate)
                .HasPrecision(10)
                .HasColumnName("avgRate");

            entity.Property(e => e.ContactInfoId)
                .HasColumnType("int(11)")
                .HasColumnName("contactInfoId");

            entity.Property(e => e.DocumentId)
                .HasColumnType("int(11)")
                .HasColumnName("documentId");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.ContactInfo)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.ContactInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_3");

            entity.HasOne(d => d.Document)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("employee_ibfk_2");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_ibfk_1");
        }
    }
}