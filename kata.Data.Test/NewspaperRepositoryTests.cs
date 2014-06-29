using kata.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace kata.Data.Test
{
	[TestClass]
	public class NewspaperRepositoryTests
	{
		private INewspaperRepository _repo = new NewspaperRepository(new KataContext());

		[TestMethod]
		public void CanGetAllNewspapers()
		{

			var results = _repo.GetAll();

			Assert.IsTrue(results.Count() > 1);
		}

		[TestMethod]
		public void CanGetSingleNewspaper()
		{
			var results = _repo.GetAll();
			var item = results.FirstOrDefault();
			if (item == null)
				Assert.Inconclusive();

			var result = _repo.Get(item.Id);
			Assert.AreEqual(item.Id, result.Id);
			Assert.AreEqual(item.Description, result.Description);
		}

		[TestMethod]
		public void CanInsertNewspaper()
		{
			var item = new Newspaper()
			{
				Name = "Test Newspaper",
				Description = "Test Newspaper"
			};

			var result = _repo.Insert(item);
			var expected = _repo.Get(result.Id);
			Assert.AreEqual(result.Name, expected.Name);
			Assert.AreEqual(result.Description, expected.Name);
		}

		[TestMethod]
		public void CanUpdateNewspaper()
		{
			
			var item = _repo.Find(c => c.Description.ToLower().Contains("to be updated"));
			if (item == null)
			{
				var create = new Newspaper()
				{
					Name = "Test Newspaper",
					Description = "To be updated"
				};
				item = _repo.Insert(create);
			}

			var description =  "to be updated " + DateTime.Now;
			item.Description = description;
			var expected = _repo.Update(item, item.Id);

			Assert.AreEqual(description, expected.Description);
		}

		[TestMethod]
		public void CanDeleteNewspaper()
		{
			var item = new Newspaper()
			{
				Name = "To be deleted",
				Description = "To be deleted"
			};

			item = _repo.Insert(item);

			if (item == null)
				Assert.Fail("could not insert record to be deleted.");

			var result = _repo.Delete(item.Id);

			Assert.IsTrue(result);
		}
	}
}
