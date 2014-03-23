using System;
using NHibernate;

namespace MVC4.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ISession Session { get; }
		void Commit();
		void Rollback();
	}
}

