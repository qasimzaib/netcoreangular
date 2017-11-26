using System;
using System.Threading.Tasks;

namespace app.Core {
	public interface IUnitOfWork {
		Task CompleteAsync();
	}
}