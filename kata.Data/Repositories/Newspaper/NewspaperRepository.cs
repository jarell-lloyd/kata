using kata.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data
{
	public class NewspaperRepository : Repository<Newspaper>, INewspaperRepository 
	{
		public NewspaperRepository(KataContext context) 
			: base(context)
		{}
	}
}
