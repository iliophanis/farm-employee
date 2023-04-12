using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class SubEmployeeMap : IEntityTypeConfiguration<SubEmployee>
    {
        public void Configure(EntityTypeBuilder<SubEmployee> entity)
        {
            entity.ToTable("subemployee");
            entity.HasIndex(e => e.ContactInfoId, "contactInfoId");

            entity.HasIndex(e => e.EmployeeRequestId, "employeeRequestId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.ContactInfoId)
                .HasColumnType("int(11)")
                .HasColumnName("contactInfoId");

            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");

            entity.Property(e => e.EmployeeRequestId)
                .HasColumnType("int(11)")
                .HasColumnName("employeeRequestId");

            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("firstName");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("lastName");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.ContactInfoId)
                .HasColumnType("int(11)")
                .HasColumnName("contactInfoId");


            entity.HasOne(d => d.ContactInfo)
                .WithMany(p => p.SubEmployees)
                .HasForeignKey(d => d.ContactInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subemployee_ibfk_2");

            entity.HasOne(d => d.EmployeeRequest)
                .WithMany(p => p.SubEmployees)
                .HasForeignKey(d => d.EmployeeRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("subemployee_ibfk_1");
        }
    }
}