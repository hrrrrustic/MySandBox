using Microsoft.EntityFrameworkCore;

namespace Test
{
    public class MyContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }
        public MyContext(DbContextOptions<MyContext> options)
            : base(options)
        { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=oopladbstore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopProduct>()
                .HasKey(k => new { k.ShopId, k.ProductId });

            modelBuilder.Entity<Product>()
                .HasKey(k => k.ProductId);

            modelBuilder.Entity<Product>()
                .HasKey(k => k.ProductName);

            modelBuilder.Entity<Shop>()
                .HasKey(k => k.ShopId);
            modelBuilder.Entity<Shop>()
                .HasKey(k => new { k.ShopName, k.ShopAddress });
        }
    }
}