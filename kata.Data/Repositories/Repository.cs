using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		private ILog logger = LogManager.GetLogger("repository");

		protected KataContext _ctx;
		public Repository(KataContext context)
		{
			_ctx = context;
		}

		public IQueryable<TEntity> GetAll()
		{
			try
			{
				var results = _ctx.Set<TEntity>().AsQueryable<TEntity>();
				return results;
			} 
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return new List<TEntity>().AsQueryable();
			}
		}

		public TEntity Get(int id)
		{
			try
			{
				var result = _ctx.Set<TEntity>().Find(id);
				return result;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return null;
			}
		}

		public TEntity Find(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var result = _ctx.Set<TEntity>().SingleOrDefault(predicate);
				return result;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return null;
			}
		}
		public ICollection<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
		{
			try
			{
				var result =_ctx.Set<TEntity>().Where(predicate).ToList();
				return result;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return null;
			}
		}
		public TEntity Insert(TEntity entity)
		{
			try
			{
				_ctx.Set<TEntity>().Add(entity);
				_ctx.SaveChanges();

				return entity;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return null;
			}
			
		}

		public TEntity Update(TEntity entity, int key)
		{
			if (entity == null)
				return null;

			try
			{
				TEntity existing = _ctx.Set<TEntity>().Find(key);
				if (existing != null)
				{
					_ctx.Entry(existing).CurrentValues.SetValues(entity);
					_ctx.SaveChanges();

					return existing;
				}
				else return null;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return null;
			}
		}

		public bool Delete(int key)
		{
			if (key < 1)
				return false;

			try
			{
				var entity = _ctx.Set<TEntity>().Find(key);
				_ctx.Set<TEntity>().Remove(entity);
				_ctx.SaveChanges();
				return true;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return false;
			}
		}

		public int Count()
		{
			try
			{
				var result = _ctx.Set<TEntity>().Count<TEntity>();
				return result;
			}
			catch (Exception e)
			{
				logger.Error(e.StackTrace);
				return 0;
			}
		}
	}
}
