using kata.Data;
using kata.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kataWebApi.Controllers
{
	public class BaseController<T> : ApiController where T : BaseEntity
	{

		private IRepository<T> _repo;

		public IEnumerable<T> GetAll()
		{
			var result = _repo.GetAll();

			return result;
		}

		public T Get(int id)
		{
			var item = _repo.Get(id);
			if (item == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			return item;
		}

		public HttpResponseMessage Post(T item)
		{
			var result = _repo.Insert(item);
			var response = Request.CreateResponse<T>(HttpStatusCode.Created, item);

			string uri = Url.Link("DefaultApi", new { id = result.Id });
			response.Headers.Location = new Uri(uri);
			return response;
		}

		public void Put(int id, T item)
		{
			item.Id = id;
			var existing = _repo.Get(id);
			if (existing == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_repo.Update(item, id);
		}

		public void Delete(int id)
		{
			var item = _repo.Get(id);
			if (item == null)
				throw new HttpResponseException(HttpStatusCode.NotFound);

			_repo.Delete(id);
		}

		public BaseController(IRepository<T> repository)
		{
			_repo = repository;
		}
	}
}
