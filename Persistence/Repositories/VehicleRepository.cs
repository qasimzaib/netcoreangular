using System.Threading.Tasks;
using app.Core.Models;
using app.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

		public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery vehicleQuery) {
			var query = context.Vehicles
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.AsQueryable();

			if (vehicleQuery.MakeId.HasValue) {
				query = query.Where(v => v.Model.MakeId == vehicleQuery.MakeId.Value);
			}
			if (vehicleQuery.ModelId.HasValue) {
				query = query.Where(v => v.Model.Id == vehicleQuery.ModelId.Value);
			}

			if (vehicleQuery.SortBy == "make") {
				query = (vehicleQuery.IsSortAscending) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);
			}

			if (vehicleQuery.SortBy == "model") {
				query = (vehicleQuery.IsSortAscending) ? query.OrderBy(v => v.Model.Name) : query.OrderByDescending(v => v.Model.Name);
			}

			if (vehicleQuery.SortBy == "contactName") {
				query = (vehicleQuery.IsSortAscending) ? query.OrderBy(v => v.ContactName) : query.OrderByDescending(v => v.ContactName);
			}

			if (vehicleQuery.SortBy == "id") {
				query = (vehicleQuery.IsSortAscending) ? query.OrderBy(v => v.Id) : query.OrderByDescending(v => v.Id);
			}

			return await query.ToListAsync();
		}

		public void Add(Vehicle vehicle) {
			context.Vehicles.Add(vehicle);
		}

		public void Remove(Vehicle vehicle) {
			context.Remove(vehicle);
		}
	}
}