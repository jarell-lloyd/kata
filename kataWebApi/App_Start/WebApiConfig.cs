using kata.Data;
using kata.Data.Entities;
using kataWebApi.Filters;
using Microsoft.Data.Edm;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace kataWebApi
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services
			var container = new UnityContainer();
			container.RegisterType<INewspaperRepository, NewspaperRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IAdRepository, AdRepository>(new HierarchicalLifetimeManager());
			config.DependencyResolver = new UnityResolver(container);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional }
			);

			config.Filters.Add(new ValidateModelAttribute());
		}
	}
}
