using System;

namespace MVC4.Models
{
	public class Location
	{
		public virtual int Id { get;  set; }
		public virtual DateTime Timestamp { get; set; }
		public virtual double Latitude { get; set; }
		public virtual double Longitude { get; set; }
		public virtual Truck Truck { get; set; }
	}
}

