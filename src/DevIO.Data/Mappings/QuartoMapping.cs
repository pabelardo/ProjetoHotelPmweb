using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings;

public class QuartoMapping : IEntityTypeConfiguration<Quarto>
{
    public void Configure(EntityTypeBuilder<Quarto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
               .IsRequired()
               .HasColumnType("varchar(200)");

        builder.Property(p => p.NumeroOcupantes)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(p => p.NumeroDeAdultos)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(p => p.NumeroDeCriancas)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(p => p.Preco)
               .IsRequired()
               .HasColumnType("decimal")
               .HasPrecision(18, 2);

        builder.Property(p => p.Fotos)
               .HasColumnType("varchar(100)");

        builder.ToTable("QUARTOS");
    }
}