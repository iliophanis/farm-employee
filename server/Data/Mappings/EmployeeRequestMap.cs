using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class EmployeeRequestMap : IEntityTypeConfiguration<EmployeeRequest>
    {
        public void Configure(EntityTypeBuilder<EmployeeRequest> entity)
        {
            entity.ToTable("employee_request");

            entity.HasIndex(e => e.EmployeeId, "employeeId");

            entity.HasIndex(e => e.PackageId, "packageId");

            entity.HasIndex(e => e.RequestId, "requestId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.EmployeeId)
                .HasColumnType("int(11)")
                .HasColumnName("employeeId");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.MessageSent)
                .HasColumnName("messageSent")
                .HasDefaultValueSql("'0'");

            entity.Property(e => e.PackageId)
                .HasColumnType("int(11)")
                .HasColumnName("packageId");

            entity.Property(e => e.PaymentMethod)
                .HasColumnType("enum('bankTransfer','paypal','ebanking')")
                .HasColumnName("paymentMethod");

            entity.Property(e => e.PaymentStatus)
                .IsRequired()
                .HasColumnType("enum('pendingPayment','processing','onHold','completed','canceled','refunded','failed')")
                .HasColumnName("paymentStatus");

            entity.Property(e => e.RequestId)
                .HasColumnType("int(11)")
                .HasColumnName("requestId");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeeRequests)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_request_ibfk_1");

            entity.HasOne(d => d.Package)
                .WithMany(p => p.EmployeeRequests)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_request_ibfk_3");

            entity.HasOne(d => d.Request)
                .WithMany(p => p.EmployeeRequests)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employee_request_ibfk_2");
        }
    }
}