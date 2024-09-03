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

            // Configure the uniqueness of the email column
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure CreatedAt and UpdatedAt columns as 'timestamp without time zone'
            // This is done so that the code does not work with UTC and does not add 5 hours to Colombia
            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasColumnType("timestamp without time zone");
        }
    }
}
