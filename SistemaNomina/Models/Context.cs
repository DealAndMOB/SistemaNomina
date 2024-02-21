using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;
#pragma warning disable CS8618

namespace SistemaNomina.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
