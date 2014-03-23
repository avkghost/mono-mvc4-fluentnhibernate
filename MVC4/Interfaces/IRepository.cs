using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace MVC4.Interfaces
{
	public interface IReadOnlyRepository<TEntity> where TEntity:class
	{
		IQueryable<TEntity> All();
		TEntity FindBy(Expression<Func<TEntity, bool>> expression);
		IQueryable<TEntity> FilterBy(Expression<Func<TEntity, bool>> expression);
	}

	public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity:class
	{
		bool Add(TEntity entity);
		bool Add(IEnumerable<TEntity> items);
		bool Update(TEntity entity);
		bool Delete(TEntity entity);
		bool Delete(IEnumerable<TEntity> entities);
	}

	public interface IIntKeyedRepository<TEntity> : IRepository<TEntity> where TEntity:class
	{
		TEntity FindBy(int id);
	}
}

