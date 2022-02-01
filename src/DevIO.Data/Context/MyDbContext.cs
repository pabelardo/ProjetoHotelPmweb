using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Quarto> Quartos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetProperties()
                         .Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)"); //Caso o atributo não seja mapeado, ele será do tipo varchar(100) no SQL

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly); /* Ele vai pegar o DbContext e buscar todas as entidades 
                                                                                         * mapeadas no mesmo, e buscar classes que herdem de IEntityTypeConfiguration
                                                                                         * para as que estão relacionadas no DbContext, eai vai registrar de uma vez só.
                                                                                         * Tudo via reflection. */

        //Evitando que uma classe representada no banco ao excluir, leve os filhos juntos.
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);
    }
}