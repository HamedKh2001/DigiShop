using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataLayer.Context
{
	public class ShopContext : DbContext
	{
		public ShopContext(DbContextOptions<ShopContext> options) : base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<GroupProduct> GroupProducts { get; set; }
		public DbSet<Productcomment> Productcomments { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<ProductPhoto> ProductPhotos { get; set; }
		public DbSet<ProductColor> ProductColors { get; set; }
		public DbSet<ProductPrice> ProductPrices { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<BannerPhoto> BannerPhotos { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Amazing_Suggest> Amazing_Suggests { get; set; }
		public DbSet<ProductsOfGroupsIIndex> productsOfGroupsIIndices { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
				
			}
			base.OnModelCreating(modelBuilder);
		}
	}
}
