using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class HotelMapping : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
               .IsRequired()
               .HasColumnType("varchar(200)");

        builder.Property(p => p.CNPJ)
               .IsRequired()
               .HasColumnType("char(14)");

        builder.Property(p => p.Endereco)
               .IsRequired()
               .HasColumnType("varchar(100)");

        builder.Property(p => p.Descricao)
               .IsRequired()
               .HasColumnType("varchar(500)");

        builder.Property(p => p.Fotos)
               .HasColumnType("varchar(100)");

        // 1 : N => Hotel : Quarto

        builder.HasMany(h => h.Quartos)
            .WithOne(q => q.Hotel)
            .HasForeignKey(q => q.HotelId);

        builder.ToTable("HOTELS");
    }
}