using System;
using System.Threading.Tasks;
using app.Controllers.Resources;
using app.Interfaces;
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
		private readonly IVehicleRepository repository;

		public VehiclesController(AppDbContext context, IMapper mapper, IVehicleRepository repository) {
			this.repository = repository;
			this.context = context;
			this.mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
			vehicle.LastUpdated = DateTime.Now;
			context.Vehicles.Add(vehicle);
			await context.SaveChangesAsync();

			vehicle = await repository.GetVehicle(vehicle.Id);

			var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var vehicle = await repository.GetVehicle(id);

			if (vehicle == null) {
				return NotFound(id);
			}
			mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
			vehicle.LastUpdated = DateTime.Now;

			await context.SaveChangesAsync();

			var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVehicle(int id) {
			var vehicle = await context.Vehicles.FindAsync(id);
			if (vehicle == null) {
				return NotFound(id);
			}
			context.Remove(vehicle);
			await context.SaveChangesAsync();
			return Ok(id);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetVehicle(int id) {
			var vehicle = await repository.GetVehicle(id);

			if (vehicle == null) {
				return NotFound(id);
			}
			var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);
			return Ok(vehicleResource);
		}
	}
}