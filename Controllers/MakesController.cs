using System.Collections.Generic;
using System.Threading.Tasks;
using app.Models;
using app.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers {
	public class MakesController : Controller {
		private readonly AppDbContext context;

		public MakesController(AppDbContext context) {
			this.context = context;
		}

		[HttpGet("/api/makes")]
		public async Task<IEnumerable<Make>> GetMakes() {
			return await context.Makes.Include(m => m.Models).ToListAsync();
		}
	}
}