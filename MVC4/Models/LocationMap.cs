using System;
using FluentNHibernate.Mapping;

namespace MVC4.Models
{
	public class LocationMap : ClassMap<Location>
	{
		public LocationMap()
		{
			Table("locations");
			Id(x => x.Id);
			Map(x => x.Timestamp).Column("DateTime");
			Map(x => x.Latitude);
			Map(x => x.Longitude);
			References(x => x.Truck).Column("TruckId");
		}
	}
}

