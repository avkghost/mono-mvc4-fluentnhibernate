using System;
using FluentNHibernate.Mapping;

namespace MVC4.Models
{
	public class DriverMap : ClassMap<Driver>
	{
		public DriverMap()
		{
			Table("drivers");
			Id(x => x.Id);
			Map(x => x.FirstName);
			Map(x => x.LastName);
			References(x => x.Truck).Column("TruckId");
		}
	}
}

