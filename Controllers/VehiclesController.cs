using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using app.Controllers.Resources;
using app.Core;
using app.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
	[Route("/api/vehicles")]
	public class VehiclesController : Controller {
		private readonly IMapper mapper;
		private readonly IVehicleRepository repository;
		private readonly IUnitOfWork unitOfWork;

		public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork) {
			this.repository = repository;
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
		}

		[HttpPost]
		public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource) {
			if (!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
			vehicle.LastUpdated = DateTime.Now;

			repository.Add(vehicle);
			await unitOfWork.CompleteAsync();

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

			await unitOfWork.CompleteAsync();

			vehicle = await repository.GetVehicle(vehicle.Id);
			var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteVehicle(int id) {
			var vehicle = await repository.GetVehicle(id, includeRelated: false);
			if (vehicle == null) {
				return NotFound(id);
			}
			repository.Remove(vehicle);
			await unitOfWork.CompleteAsync();
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

		[HttpGet]
		public async Task<IEnumerable<VehicleResource>> GetVehicles(VehicleQueryResource vehicleQueryResource) {
			var vehicleQuery = mapper.Map<VehicleQueryResource, VehicleQuery>(vehicleQueryResource);
			var vehicles = await repository.GetVehicles(vehicleQuery);
			return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleResource>>(vehicles);
		}
	}
}