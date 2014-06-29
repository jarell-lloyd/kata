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
	public class AdControllerTests
	{
		[TestMethod]
		public void CanCallGetAll()
		{
			var ads = new List<Ad>() {
				new Ad() { Id = 1, Name = "1", Description = "1"},
				new Ad() { Id = 2, Name = "2", Description = "2"}
			}.AsQueryable();

			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.GetAll()).Returns(ads);
			//mock.Setup(x => x.GetCustomerTotal(It.IsAny<int>())).Returns(25.5);

			var controller = new AdsController(mock.Object);

			var results = controller.GetAll();

			Assert.IsTrue(results.Count() > 1);
			Assert.IsTrue(results.First().Id == 1);
			Assert.IsTrue(results.Last().Description == "2");
		}

		[TestMethod]
		public void CanCallGet()
		{
			var ad = new Ad()
			{
				Id = 10,
				Name = "10",
				Description = "10"
			};

			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns(ad);

			var controller = new AdsController(mock.Object);

			var result = controller.Get(10);

			Assert.IsTrue(result.Name == "10");
		}

		[TestMethod]
		public void CanCallInsert()
		{
			var ad = new Ad()
			{
				Name = "20",
				Description = "20"
			};

			var newAd = new Ad()
			{
				Id = 20,
				Name = "20",
				Description = "20"
			};


			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.Insert(It.IsAny<Ad>())).Returns(newAd);

			var controller = new AdsController(mock.Object);
			controller.Request = new HttpRequestMessage
			{
				RequestUri = new Uri("http://localhost/api/ads")
			};
			controller.Configuration = new HttpConfiguration();
			controller.Configuration.Routes.MapHttpRoute(
					name: "DefaultApi",
					routeTemplate: "api/{controller}/{id}",
					defaults: new { id = RouteParameter.Optional });

			controller.RequestContext.RouteData = new HttpRouteData(
					route: new HttpRoute(),
					values: new HttpRouteValueDictionary { { "controller", "ads" } });

			var response = controller.Post(ad);
			var expectedUrl = "http://localhost/api/ads/" + newAd.Id;
			Assert.IsNotNull(response);
			Assert.AreEqual(expectedUrl, response.Headers.Location.AbsoluteUri);
		}

		[TestMethod]
		public void CanCallUpdate()
		{
			var ad = new Ad()
			{
				Id = 30,
				Name = "30",
				Description = "30"
			};

			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.Update(It.IsAny<Ad>(), It.IsAny<int>())).Returns<Ad>(null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns(ad);
			var controller = new AdsController(mock.Object);

			controller.Put(ad.Id, ad);

			mock.Verify(repo => repo.Update(It.IsAny<Ad>(), It.IsAny<int>()), Times.AtLeastOnce());

		}

		[TestMethod]
		[ExpectedException(typeof(HttpResponseException))]
		public void ShouldThrowDuringUpdateIfEntityDoesNotExist()
		{
			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.Update(It.IsAny<Ad>(), It.IsAny<int>())).Returns((Ad)null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns((Ad)null);

			var controller = new AdsController(mock.Object);

			controller.Put(1, new Ad());
		}

		[TestMethod]
		public void CanCallDelete()
		{

			var ad = new Ad()
			{
				Id = 40,
				Name = "40",
				Description = "40"
			};

			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.Delete(It.IsAny<int>())).Returns<Ad>(null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns(ad);
			var controller = new AdsController(mock.Object);

			controller.Delete(ad.Id);

			mock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.AtLeastOnce());

		}

		[TestMethod]
		[ExpectedException(typeof(HttpResponseException))]
		public void ShouldThrowDuringDeleteIfEntityDoesNotExist()
		{
			var mock = new Mock<IAdRepository>();
			mock.Setup(c => c.Update(It.IsAny<Ad>(), It.IsAny<int>())).Returns((Ad)null);
			mock.Setup(c => c.Get(It.IsAny<int>())).Returns((Ad)null);

			var controller = new AdsController(mock.Object);

			controller.Delete(1);
		}

	}
}
