using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class ContactInfoMap : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> entity)
        {
            entity.ToTable("contact_info");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("address");

            entity.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("city");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.MobilePhoneNo)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("mobilePhoneNo");

            entity.Property(e => e.PhoneNo)
                .HasMaxLength(255)
                .HasColumnName("phoneNo");

            entity.Property(e => e.Tk)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("tk");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("updateDate")
                .HasDefaultValueSql("current_timestamp()");
        }
    }
}