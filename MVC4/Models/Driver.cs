using System;

namespace MVC4.Models
{
	public class Driver
	{
		public virtual int Id { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Truck Truck { get; set; }
	}
}

