using System.Collections.Generic;
using System.Threading.Tasks;
using app.Core.Models;

namespace app.Core {
	public interface IPhotoRepository {
		Task<IEnumerable<Photo>> GetPhotos (int vehicleId);
	}
}