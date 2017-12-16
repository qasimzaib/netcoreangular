using app.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Persistence {
	public class AppDbContext : DbContext {
		public DbSet<Feature> Features { get; set; }
		public DbSet<Make> Makes { get; set; }
		public DbSet<Model> Models { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<Photo> Photos { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
			
		}

		protected override void OnModelCreating (ModelBuilder modelBuilder) {
			modelBuilder.Entity<VehicleFeature>().HasKey(vf => new { vf.VehicleId, vf.FeatureId });
		}
	}
}