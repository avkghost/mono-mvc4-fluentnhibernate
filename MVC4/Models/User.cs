using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC4.Models
{
	public class User
	{
		public virtual Guid ID { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string MidName { get; set; }
		public virtual string LastName { get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public virtual DateTime EnrollmentDate { get; set; }

	}
}

