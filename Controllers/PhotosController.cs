using System;
using System.IO;
using System.Threading.Tasks;
using app.Controllers.Resources;
using app.Core;
using app.Core.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers {
	[Route("/api/vehicles/{vehicleId}/photos")]
	public class PhotosController : Controller {
		private readonly IHostingEnvironment host;
		private readonly IMapper mapper;
		private readonly IUnitOfWork unitOfWork;
		private readonly IVehicleRepository vehicleRepository;

		public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork, IMapper mapper) {
			this.host = host;
			this.vehicleRepository = vehicleRepository;
			this.unitOfWork = unitOfWork;
			this.mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Upload(int vehicleId, IFormFile file) {
			var vehicle = await vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
			if (vehicle == null) {
				return NotFound();
			}

			var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads/" + vehicleId);
			if (!Directory.Exists(uploadFolderPath)) {
				Directory.CreateDirectory(uploadFolderPath);
			}

			var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var uploadFilePath = Path.Combine(uploadFolderPath, fileName);

			using (var stream = new FileStream(uploadFilePath, FileMode.Create)) {
				await file.CopyToAsync(stream);
			}

			var photo = new Photo {
				FileName = fileName
			};

			vehicle.Photos.Add(photo);
			await unitOfWork.CompleteAsync();

			return Ok(mapper.Map<Photo, PhotoResource>(photo));
		}
	}
}