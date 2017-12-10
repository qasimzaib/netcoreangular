using System.Threading.Tasks;
using app.Core.Models;
using app.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace app.Persistence.Repositories {
	public class VehicleRepository : IVehicleRepository {
		private readonly AppDbContext context;
		
		public VehicleRepository(AppDbContext context) {
			this.context = context;
		}

		public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true) {
			if (!includeRelated) {
				return await context.Vehicles.FindAsync(id);
			}

			return await context.Vehicles
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.SingleOrDefaultAsync(v => v.Id == id);
		}

		public async Task<IEnumerable<Vehicle>> GetVehicles() {
			return await context.Vehicles
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.ToListAsync();
		}

		public void Add(Vehicle vehicle) {
			context.Vehicles.Add(vehicle);
		}

		public void Remove(Vehicle vehicle) {
			context.Remove(vehicle);
		}
	}
}