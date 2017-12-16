using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Core;
using app.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace app.Persistence.Repositories {
	public class PhotoRepository : IPhotoRepository {
		private readonly AppDbContext context;
		
		public PhotoRepository(AppDbContext context) {
			this.context = context;
		}

		public async Task<IEnumerable<Photo>> GetPhotos (int vehicleId) {
			return await context.Photos
				.Where(p => p.VehicleId == vehicleId)
				.ToListAsync();
		}
	}
}