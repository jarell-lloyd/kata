using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data
{
	public interface IRepository<TEntity> where TEntity : class
	{
		IQueryable<TEntity> GetAll();
		TEntity Get(int id);
		TEntity Find(Expression<Func<TEntity, bool>> predicate);
		ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
		TEntity Insert(TEntity entity);
		TEntity Update(TEntity entity, int key);
		bool Delete(int key);
		int Count();
	}
}
