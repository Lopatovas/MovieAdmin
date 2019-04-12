using Microsoft.EntityFrameworkCore;
using MovieAdministrator.Models;

namespace MovieAdministrator.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Cast>().ToTable("Cast");
            modelBuilder.Entity<Genre>().ToTable("Genre");
            modelBuilder.Entity<Movie>().ToTable("Movie");
        }
    }
}
