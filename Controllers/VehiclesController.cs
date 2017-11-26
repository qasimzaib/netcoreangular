using System;
using System.Threading.Tasks;
using app.Controllers.Resources;
using app.Models;
using app.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers {
	[Route("/api/vehicles")]
	public class VehiclesController : Controller {
		private readonly AppDbContext context;
		private readonly IMapper mapper;
		
		public VehiclesController(AppDbContext context, IMapper mapper) {
			this.context = context;
			this.mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
			vehicle.LastUpdated = DateTime.Now;
			context.Vehicles.Add(vehicle);
			await context.SaveChangesAsync();
			var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleResource vehicleResource) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var vehicle = await context.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);
			mapper.Map<VehicleResource, Vehicle>(vehicleResource, vehicle);
			vehicle.LastUpdated = DateTime.Now;

			await context.SaveChangesAsync();
			var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
			return Ok(result);
		}
	}
}