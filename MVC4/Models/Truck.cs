using System;
using System.Collections.Generic;

namespace MVC4.Models
{
	public class Truck
	{
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
		public virtual string Type { get; set; }
		public virtual string PlateNumber { get; set; }
		public virtual Driver Driver { get; set; }
		public virtual IList<Location> Locations { get; set; }

		public Truck()
		{
			Locations = new List<Location>();
		}

		public virtual void AddDriver(Driver driver)
		{
			driver.Truck = this;
			Driver = driver;
		}

		public virtual void AddLocation(Location location)
		{
			location.Truck = this;
			Locations.Add(location);
		}
	}
}

