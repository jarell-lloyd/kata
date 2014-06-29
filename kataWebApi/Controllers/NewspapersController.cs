using kata.Data;
using kata.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;

namespace kataWebApi.Controllers
{
	public class NewspapersController : BaseController<Newspaper>
	{
		public NewspapersController(INewspaperRepository repository)
			: base(repository)
		{ }
	}
}
