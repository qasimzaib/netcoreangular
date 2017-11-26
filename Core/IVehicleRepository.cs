using System.Threading.Tasks;
using app.Core.Models;

namespace app.Core {
	public interface IVehicleRepository {
		Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
		void Add(Vehicle vehicle);
		void Remove(Vehicle vehicle);
	}
}