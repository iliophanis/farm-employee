using Microsoft.EntityFrameworkCore.Metadata.Builders;
using server.Data.Entities;

namespace server.Data.Mappings
{
    public class CultivationMap : IEntityTypeConfiguration<Cultivation>
    {
        public void Configure(EntityTypeBuilder<Cultivation> entity)
        {
            entity.ToTable("cultivation");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");

            entity.Property(e => e.InsertDate)
                .HasColumnType("timestamp")
                .ValueGeneratedOnAddOrUpdate()
                .HasColumnName("insertDate")
                .HasDefaultValueSql("current_timestamp()");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.UpdateDate)
                .HasColumnType("timestamp")
                .HasColumnName("updateDate")
                .HasDefaultValueSql("'0000-00-00 00:00:00'");
        }
    }
}