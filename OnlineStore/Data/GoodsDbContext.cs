using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data {
    public class StoreDbContext : DbContext {
        public DbSet<Product> Products { get; protected set; }
        public DbSet<Category> Categories { get; protected set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) {
            Database.EnsureCreated();
        }
    }
}
