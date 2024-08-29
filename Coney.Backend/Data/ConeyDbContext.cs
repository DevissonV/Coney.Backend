using Coney.Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coney.Backend.Data
{
    public class ConeyDbContext : DbContext
    {
        public ConeyDbContext(DbContextOptions<ConeyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la unicidad del correo electrónico
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
