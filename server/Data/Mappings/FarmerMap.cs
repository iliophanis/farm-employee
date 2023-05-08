using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class FarmerMap : IEntityTypeConfiguration<Farmer>
    {
        public void Configure(EntityTypeBuilder<Farmer> entity)
        {
            entity.ToTable("farmer");

            entity.HasIndex(e => e.ContactInfoId, "contactInfoId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.AvgPaymentConsequenceRate)
                .HasPrecision(10)
                .HasColumnName("avgPaymentConsequenceRate");

            entity.Property(e => e.AvgRate)
                .HasPrecision(10)
                .HasColumnName("avgRate");

            entity.Property(e => e.AvgWorkPlaceRate)
                .HasPrecision(10)
                .HasColumnName("avgWorkPlaceRate");

            entity.Property(e => e.ContactInfoId)
                .HasColumnType("int(11)")
                .HasColumnName("ContactInfoId");

            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.PaymentMethod)
                .HasColumnType("enum('bankTransfer','paypal','ebanking')")
                .HasColumnName("paymentMethod");

            entity.Property(e => e.PaymentStatus)
                .HasColumnType("enum('pendingPayment','processing','onHold','completed','canceled','refunded','failed')")
                .HasColumnName("paymentStatus");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.ContactInfo)
                .WithMany(p => p.Farmers)
                .HasForeignKey(d => d.ContactInfoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmer_ibfk_2");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Farmers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("farmer_ibfk_1");
        }
    }
}