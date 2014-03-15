using FluentNHibernate.Mapping; 

namespace MVC4.Models
{
	public class UserMap : ClassMap<User>
	{
		public UserMap ()
		{
			Id (x => x.ID);
			Map (x => x.FirstName);
			Map (x => x.MidName);
			Map (x => x.LastName);
			Map (x => x.EnrollmentDate);
			Table ("users");
		}
	}
}

