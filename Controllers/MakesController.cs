using System.Collections.Generic;
using System.Threading.Tasks;
using app.Controllers.Resources;
using app.Models;
using app.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers {
	public class MakesController : Controller {
		private readonly AppDbContext context;
		private readonly IMapper mapper;

		public MakesController(AppDbContext context, IMapper mapper) {
			this.mapper = mapper;
			this.context = context;
		}

		[HttpGet("/api/makes")]
		public async Task<IEnumerable<MakeResource>> GetMakes() {
			var makes = await context.Makes.Include(m => m.Models).ToListAsync();
			return mapper.Map<List<Make>, List<MakeResource>>(makes);
		}
	}
}