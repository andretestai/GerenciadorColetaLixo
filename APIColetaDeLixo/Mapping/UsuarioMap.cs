using APIColetaDeLixo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIColetaDeLixo.Mapping
{
    public class UsuarioMap
    {
        public static void Map(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().ToTable("TB_Usuario")
        .HasKey(u => u.Id);

            modelBuilder.Entity<UsuarioModel>().Property(u => u.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<UsuarioModel>().Property(u => u.Nome)
                .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<UsuarioModel>().Property(u => u.Email)
                .IsRequired().HasMaxLength(100);

            modelBuilder.Entity<UsuarioModel>().Property(u => u.Telefone)
                .HasMaxLength(15);

            modelBuilder.Entity<UsuarioModel>().HasMany(u => u.Coletas)
                .WithOne(c => c.Usuario).HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
