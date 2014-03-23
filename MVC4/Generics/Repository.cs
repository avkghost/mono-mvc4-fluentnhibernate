using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using MVC4.Interfaces;

namespace MVC4.Generics
{
	public class Repository<T> : NHibernate.Context.CurrentSessionContext, IIntKeyedRepository<T> where T : class
	{
		private ISession _session;

		protected override ISession Session {
			get{
				return _session;
			}
			set{
				_session = Session;
			}
		}

		public Repository(ISession session)
		{
			_session = session;
		}

		#region IRepository<T> Members

		public bool Add(T entity)
		{
			_session.Save(entity);
			return true;
		}

		public bool Add(System.Collections.Generic.IEnumerable<T> items)
		{
			foreach (T item in items)
			{
				_session.Save(item);
			}
			return true;
		}

		public bool Update(T entity)
		{
			_session.Update(entity);
			return true;
		}

		public bool Delete(T entity)
		{
			_session.Delete(entity);
			return true;
		}

		public bool Delete(System.Collections.Generic.IEnumerable<T> entities)
		{
			foreach (T entity in entities)
			{
				_session.Delete(entity);
			}
			return true;
		}

		#endregion

		#region IIntKeyedRepository<T> Members

		public T FindBy(int id)
		{
			return _session.Get<T>(id);
		}

		#endregion

		#region IReadOnlyRepository<T> Members

		public IQueryable<T> All()
		{
			return _session.Query<T>();
		}

		public T FindBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
		{
			return FilterBy(expression).Single();
		}

		public IQueryable<T> FilterBy(System.Linq.Expressions.Expression<System.Func<T, bool>> expression)
		{
			return All().Where(expression).AsQueryable();
		}

		#endregion

	}
}

