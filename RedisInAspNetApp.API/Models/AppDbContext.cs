using Microsoft.EntityFrameworkCore;

namespace RedisInAspNetApp.API.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>().HasData(
				new Product { Id = 1, Name = "Test", Price = 100, },
				new Product { Id = 2, Name = "Test2", Price = 100, },
				new Product { Id = 3, Name = "Test3", Price = 100, });

			base.OnModelCreating(modelBuilder);
		}
	}
}
