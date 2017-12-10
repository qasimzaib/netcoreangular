using System.Threading.Tasks;
using app.Core.Models;
using app.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System;

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

			var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>() {
				["make"] = v => v.Model.Make.Name,
				["model"] = v => v.Model.Name,
				["contactName"] = v => v.ContactName,
				["id"] = v => v.Id
			};

			query = ApplyOrdering(vehicleQuery, query, columnMap);
			return await query.ToListAsync();
		}

		private IQueryable<Vehicle> ApplyOrdering (VehicleQuery vehicleQuery, IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> columnMap) {
			if (vehicleQuery.IsSortAscending) {
				return query.OrderBy(columnMap[vehicleQuery.SortBy]);
			} else {
				return query.OrderByDescending(columnMap[vehicleQuery.SortBy]);
			}
		}

		public void Add(Vehicle vehicle) {
			context.Vehicles.Add(vehicle);
		}

		public void Remove(Vehicle vehicle) {
			context.Remove(vehicle);
		}
	}
}