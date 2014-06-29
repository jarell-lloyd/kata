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
    public class AdsController : BaseController<Ad>
    {
			public AdsController(IAdRepository repository)
				: base(repository)
			{ }

    }
}
