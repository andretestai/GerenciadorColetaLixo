using APIColetaDeLixo.Mapping;
using APIColetaDeLixo.Models;
using Microsoft.EntityFrameworkCore;

namespace APIColetaDeLixo.DataContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public virtual DbSet<ColetaModel> Coleta { get; set; }
        public virtual DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UsuarioMap.Map(modelBuilder);
            ColetaMap.Map(modelBuilder);
        }
    }

}
