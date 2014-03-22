using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

using MVC4.Configurations;
using MVC4.Models;

using System.Reflection;

namespace MVC4.Helpers
{
	public class NHibernateHelper
	{
		private static ISessionFactory _sessionFactory;

		private static ISessionFactory SessionFactory
		{
			get
			{
				if (null == _sessionFactory)
					InitializeSessionFactory();

				return _sessionFactory;
			}
		}

		private static void InitializeSessionFactory()
		{
			/*
			_sessionFactory = Fluently.Configure()
				.Database(MsSqlConfiguration.MsSql2008
					.ConnectionString(
					@"Server=localhost\SQLExpress;Database=SimpleNHibernate;Trusted_Connection=True;")
					.ShowSql()
				)
				.Mappings(m => m.FluentMappings
					.AddFromAssembly(Assembly.GetExecutingAssembly()))
				.ExposeConfiguration(cfg => new SchemaExport(cfg)
					.Create(true, true))
				.BuildSessionFactory();
				*/

			_sessionFactory = Fluently.Configure ()
				.Database (MonoSQLiteConfiguration.Standard
					.UsingFile (@"MVC4.sqlite")
				)
				.Mappings (m => m.FluentMappings
					.AddFromAssembly(Assembly.GetExecutingAssembly()))
				.ExposeConfiguration(cfg=>new SchemaUpdate(cfg)
					.Execute(true, true))
				.BuildSessionFactory ();

			/*	
			_sessionFactory = Fluently.Configure()
				.Database(PostgreSQLConfiguration.PostgreSQL82
					.ConnectionString(c => c
					                  .Host("localhost")
					                  .Port(5432)
					                  .Database("test")
					                  .Username("test")
					                  .Password("test")))
				.Mappings(m => m.FluentMappings
					.AddFromAssembly(Assembly.GetExecutingAssembly()))
				.ExposeConfiguration(cfg=>new SchemaUpdate(cfg)
					.Execute(true, true))
				.BuildSessionFactory();
				*/
			/*
			_sessionFactory = Fluently.Configure ()
				.Database (MySQLConfiguration.Standard
				           .ConnectionString (@"Server=localhost;Database=test;User ID=test;Password=test;")
				          .ShowSql ()
				)
				.Mappings (m => m.FluentMappings
					.AddFromAssembly(Assembly.GetExecutingAssembly()))
				.ExposeConfiguration (cfg => new SchemaUpdate (cfg)
					.Execute (true, true))
				.BuildSessionFactory ();
				*/

		}

		public static ISession OpenSession()
		{
			return SessionFactory.OpenSession();
		}
	}

}

