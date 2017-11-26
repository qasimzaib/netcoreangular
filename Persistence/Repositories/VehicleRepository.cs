using System.Threading.Tasks;
using app.Interfaces;
using app.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Persistence.Repositories {
	public class VehicleRepository : IVehicleRepository {
		private readonly AppDbContext context;
		
		public VehicleRepository(AppDbContext context) {
			this.context = context;
		}

		public async Task<Vehicle> GetVehicle(int id) {
			return await context.Vehicles
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.SingleOrDefaultAsync(v => v.Id == id);
		}
	}
}