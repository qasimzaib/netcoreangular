using System;
using System.Threading.Tasks;

namespace app.Interfaces{
	public interface IUnitOfWork {
		Task CompleteAsync();
	}
}