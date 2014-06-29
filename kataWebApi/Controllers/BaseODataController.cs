using kata.Data;
using kata.Data.Entities;
using kataWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace kataWebApi.Controllers
{
	public abstract class BaseODataController<T> : EntitySetController<T, int> where T : class
	{
		Repository<T> repo = new Repository<T>(new KataContext());

		[Queryable]
		public override IQueryable<T> Get()
		{
			var result = repo.GetAll();
			return result;
		}

		protected override T GetEntityByKey(int key)
		{
			return repo.Get(key);
		}

		protected override T CreateEntity(T entity)
		{
			return repo.Insert(entity);
		}

		public override void Delete(int key)
		{
			var result = repo.Delete(key);
			if (!result)
				throw ResponseHelper.ResourceNotFound(Request);
		}

		protected override T PatchEntity(int key, Delta<T> patch)
		{
			var entity = repo.Get(key);
			if (entity == null)
				throw ResponseHelper.ResourceNotFound(Request);

			patch.Patch(entity);
			repo.Update(entity, key);

			return entity;
		}
	}
}