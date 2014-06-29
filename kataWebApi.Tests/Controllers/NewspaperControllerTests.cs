using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using kataWebApi;
using kataWebApi.Controllers;
using kata.Data.Entities;
using kata.Data;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using System.Web.Http.Results;
using System;


namespace kataWebApi.Tests
{
	[TestClass]
	public class NewspaperControllerTests
	{
		[TestMethod]
		public void CanCallGetAll()
		{
			var newspapers = new List<Newspaper>() {
				new Newspaper() { Id = 1, Name = "1", Description = "1"},
				new Newspaper() { Id = 2, Name = "2", Description = "2"}
			}.AsQueryable();

			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.GetAll()).Returns(newspapers);

			var controller = new NewspapersController(mock.Object);

			var results = controller.GetAll();
	
			Assert.IsTrue(results.Count() > 1);
			Assert.IsTrue(results.First().Id == 1);
			Assert.IsTrue(results.Last().Description == "2");
		}

		[TestMethod]
		public void CanCallGet()
		{
			var newspaper = new Newspaper()
			{
				Id = 10,
				Name = "10",
				Description = "10"
			};

			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns(newspaper);

			var controller = new NewspapersController(mock.Object);

			var result = controller.Get(10);

			Assert.IsTrue(result.Name == "10");
		}

		[TestMethod]
		public void CanCallInsert()
		{
			var newspaper = new Newspaper()
			{
				Name = "20",
				Description = "20"
			};

			var newNewspaper = new Newspaper()
			{
				Id = 20,
				Name = "20",
				Description = "20"
			};


			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.Insert(It.IsAny<Newspaper>())).Returns(newNewspaper);

			var controller = new NewspapersController(mock.Object);
			controller.Request = new HttpRequestMessage
			{
				RequestUri = new Uri("http://localhost/api/newspapers")
			};
			controller.Configuration = new HttpConfiguration();
			controller.Configuration.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional });

			controller.RequestContext.RouteData = new HttpRouteData(
					route: new HttpRoute(),
					values: new HttpRouteValueDictionary { { "controller", "newspapers" } });

			var response = controller.Post(newspaper);
			var expectedUrl = "http://localhost/api/newspapers/" + newNewspaper.Id;
			Assert.IsNotNull(response);
			Assert.AreEqual(expectedUrl, response.Headers.Location.AbsoluteUri);
		}

		[TestMethod]
		public void CanCallUpdate()
		{
			var newspaper = new Newspaper()
			{
				Id = 30,
				Name = "30",
				Description = "30"
			};

			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.Update(It.IsAny<Newspaper>(), It.IsAny<int>())).Returns<Newspaper>(null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns(newspaper);
			var controller = new NewspapersController(mock.Object);

			controller.Put(newspaper.Id, newspaper);

			mock.Verify(repo => repo.Update(It.IsAny<Newspaper>(), It.IsAny<int>()), Times.AtLeastOnce());

		}

		[TestMethod]
		[ExpectedException(typeof(HttpResponseException))]
		public void ShouldThrowDuringUpdateIfEntityDoesNotExist()
		{
			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.Update(It.IsAny<Newspaper>(), It.IsAny<int>())).Returns((Newspaper) null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns((Newspaper) null);

			var controller = new NewspapersController(mock.Object);

			controller.Put(1, new Newspaper());
		}

		[TestMethod]
		public void CanCallDelete()
		{

			var newspaper = new Newspaper()
			{
				Id = 40,
				Name = "40",
				Description = "40"
			};

			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.Delete( It.IsAny<int>())).Returns<Newspaper>(null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns(newspaper);
			var controller = new NewspapersController(mock.Object);

			controller.Delete(newspaper.Id);

			mock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.AtLeastOnce());

		}

		[TestMethod]
		[ExpectedException(typeof(HttpResponseException))]
		public void ShouldThrowDuringDeleteIfEntityDoesNotExist()
		{
			var mock = new Mock<INewspaperRepository>();
			mock.Setup(c => c.Update(It.IsAny<Newspaper>(), It.IsAny<int>())).Returns((Newspaper) null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns((Newspaper) null);

			var controller = new NewspapersController(mock.Object);

			controller.Delete(1);
		}

	}
}
