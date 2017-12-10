using System.Threading.Tasks;
using app.Core.Models;
using app.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;
using app.Extensions;

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

		public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObject) {
			var query = context.Vehicles
				.Include(v => v.Model)
					.ThenInclude(m => m.Make)
				.Include(v => v.Features)
					.ThenInclude(vf => vf.Feature)
				.AsQueryable();

			if (queryObject.MakeId.HasValue) {
				query = query.Where(v => v.Model.MakeId == queryObject.MakeId.Value);
			}
			if (queryObject.ModelId.HasValue) {
				query = query.Where(v => v.Model.Id == queryObject.ModelId.Value);
			}

			var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>() {
				["make"] = v => v.Model.Make.Name,
				["model"] = v => v.Model.Name,
				["contactName"] = v => v.ContactName
			};

			query = query.ApplyOrdering(queryObject, columnMap);
			query = query.ApplyPaging(queryObject);
			
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