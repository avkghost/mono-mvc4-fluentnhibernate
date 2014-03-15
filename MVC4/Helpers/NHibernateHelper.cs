using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

using MVC4.Configurations;
using MVC4.Models;

namespace MVC4.Helpers
{
	public class NHibernateHelper
	{
		private static ISessionFactory _sessionFactory;

		private static ISessionFactory SessionFactory
		{
			get
			{
				if (_sessionFactory == null)
					InitializeSessionFactory();

				return _sessionFactory;
			}
		}

		private static void InitializeSessionFactory()
		{
			//			_sessionFactory = Fluently.Configure()
			//				.Database(MsSqlConfiguration.MsSql2008
			//					.ConnectionString(
			//						@"Server=localhost\SQLExpress;Database=SimpleNHibernate;Trusted_Connection=True;")
			//					.ShowSql()
			//				)
			//				.Mappings(m =>
			//					m.FluentMappings
			//					.AddFromAssemblyOf<Car>())
			//				.ExposeConfiguration(cfg => new SchemaExport(cfg)
			//					.Create(true, true))
			//				.BuildSessionFactory();

			_sessionFactory = Fluently.Configure ()
				.Database (MonoSQLiteConfiguration.Standard
					.UsingFile (@"MVC4.sqlite")
				)
				.Mappings (m => m.FluentMappings
					.AddFromAssemblyOf<User> ())
//				.ExposeConfiguration(cfg => new SchemaExport(cfg)
//					.Create(true, true))
				.ExposeConfiguration(cfg=>new SchemaUpdate(cfg)
					.Execute(true, true))
				.BuildSessionFactory ();
		}

		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}
	}

}

