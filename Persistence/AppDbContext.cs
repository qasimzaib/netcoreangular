using Microsoft.EntityFrameworkCore;

namespace app.Persistence {
	public class AppDbContext : DbContext {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
			
		}
	}
}