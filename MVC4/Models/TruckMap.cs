using System;
using FluentNHibernate.Mapping;

namespace MVC4.Models
{
	public class TruckMap : ClassMap<Truck>
	{
		public TruckMap()
		{
			Table("trucks");
			Id(x => x.Id);
			Map(x => x.Name);
			Map(x => x.Type);
			Map(x => x.PlateNumber);
			HasOne(x => x.Driver).LazyLoad().Cascade.All();
			HasMany(x => x.Locations).LazyLoad().Inverse().Cascade.All();
		}
	}
}

