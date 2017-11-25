using app.Controllers.Resources;
using app.Models;
using AutoMapper;

namespace app.Mapping {
	public class MappingProfile : Profile {
		public MappingProfile() {
			CreateMap<Make, MakeResource>();
			CreateMap<Model, ModelResource>();
		}
	}
}