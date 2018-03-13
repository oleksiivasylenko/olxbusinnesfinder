using BusinessFinder.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessFinder.Db
{
    public sealed class OlxBusinesFinderContex: DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<SearchRequest> SearchRequests { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<PagingUrl> PagingUrls { get; set; }


        public OlxBusinesFinderContex()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-V3LMLI8;Initial Catalog=OlxBusinessFinder;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(p => p.SubCategories)
                .WithOne(t => t.Category)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
