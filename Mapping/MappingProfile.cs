using System.Collections.Generic;
using System.Linq;
using app.Controllers.Resources;
using app.Models;
using AutoMapper;

namespace app.Mapping {
	public class MappingProfile : Profile {
		public MappingProfile() {
			CreateMap<Make, MakeResource>();
			CreateMap<Model, ModelResource>();
			CreateMap<Feature, FeatureResource>();
			CreateMap<Vehicle, VehicleResource>()
				.ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Email = v.ContactEmail, Name = v.ContactName, Phone = v.ContactName }))
				.ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

			CreateMap<VehicleResource, Vehicle>()
				.ForMember(v => v.Id, opt => opt.Ignore())
				.ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
				.ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
				.ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
				.ForMember(v => v.Features, opt => opt.Ignore())
				.AfterMap((vr, v) => {
					var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));
					foreach (var f in removedFeatures) {
						v.Features.Remove(f);
					}

					var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id });
					foreach (var f in addedFeatures) {
						v.Features.Add(f);
					}
				});
		}
	}
}