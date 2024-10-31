using APIColetaDeLixo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIColetaDeLixo.Mapping
{
    public class ColetaMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColetaModel>()
        .ToTable("TB_Coleta")
        .HasKey(c => c.Id);

            modelBuilder.Entity<ColetaModel>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd(); 

            modelBuilder.Entity<ColetaModel>()
                .Property(c => c.DataColeta)
                .IsRequired();

            modelBuilder.Entity<ColetaModel>()
                .Property(c => c.Local)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<ColetaModel>()
                .Property(c => c.TipoResiduo)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<ColetaModel>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Coletas)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
