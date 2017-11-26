using System.Threading.Tasks;
using app.Models;

namespace app.Interfaces {
	public interface IVehicleRepository {
		Task<Vehicle> GetVehicle(int id);
	}
}