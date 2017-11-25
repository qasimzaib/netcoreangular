using System.Collections.Generic;
using System.Threading.Tasks;
using app.Controllers.Resources;
using app.Models;
using app.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace app.Controllers {
	public class FeaturesController {
		private readonly AppDbContext context;
		private readonly IMapper mapper;

		public FeaturesController(AppDbContext context, IMapper mapper) {
			this.mapper = mapper;
			this.context = context;
		}

		[HttpGet("/api/features")]
		public async Task<IEnumerable<FeatureResource>> GetFeatures() {
			var features = await context.Features.ToListAsync();
			return mapper.Map<List<Feature>, List<FeatureResource>>(features);
		}
	}
}