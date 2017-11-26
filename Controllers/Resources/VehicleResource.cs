using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace app.Controllers.Resources {
	public class VehicleResource {
		public int Id { get; set; }
		public KeyValuePairResource Model { get; set; }
		public KeyValuePairResource Make { get; set; }
		public bool IsRegistered { get; set; }
		public ContactResource Contact { get; set; }
		public DateTime LastUpdated { get; set; }
		public ICollection<KeyValuePairResource> Features { get; set; }

		public VehicleResource() {
			Features = new Collection<KeyValuePairResource>();
		}
	}
}