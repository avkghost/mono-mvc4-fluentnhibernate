using System;

using MVC4.Helpers;

namespace MVC4
{
	public class DatabaseConfig
	{
		public static void RegisterDatabase ()
		{
			var session = NHibernateHelper.OpenSession ();
			session.Flush ();
		}
	}
}

