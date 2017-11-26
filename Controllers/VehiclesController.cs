using app.Controllers.Resources;
using app.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {
	[Route("/api/vehicles")]
	public class VehiclesController : Controller {
		private readonly IMapper mapper;
		public VehiclesController(IMapper mapper) {
			this.mapper = mapper;

		}

		[HttpPost]
		public IActionResult CreateVehicle([FromBody] VehicleResource vehicleResource) {
			var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
			return Ok(vehicle);
		}
	}
}