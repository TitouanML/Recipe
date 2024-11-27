using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Recipe
{


    public class AppDbContext : DbContext
    {
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Etape> Etapes { get; set; }
        public DbSet<Commentaire> Commentaires { get; set; }
        public DbSet<Recette> Recettes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=recettes.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recette>()
                .HasMany(r => r.Ingredients)
                .WithMany();

            modelBuilder.Entity<Recette>()
                .HasMany(r => r.Etapes)
                .WithOne();

            modelBuilder.Entity<Recette>()
                .HasMany(r => r.Commentaires)
                .WithOne();

            modelBuilder.Entity<Recette>()
                .HasOne(r => r.Categorie)
                .WithMany(c => c.Recettes);
        }
    }

}
