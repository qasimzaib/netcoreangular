using app.Models;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {
	[Route("/api/vehicles")]
	public class VehiclesController : Controller {
		public VehiclesController() {
			
		}

		[HttpPost]
		public IActionResult CreateVehicle ([FromBody] Vehicle vehicle) {
			return Ok(vehicle);
		}
	}
}