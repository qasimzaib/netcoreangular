using System.Threading.Tasks;
using app.Core;

namespace app.Persistence {
	public class UnitOfWork : IUnitOfWork {
		private readonly AppDbContext context;
		public UnitOfWork(AppDbContext context) {
			this.context = context;
		}
		public async Task CompleteAsync() {
			await context.SaveChangesAsync();
		}
	}
}