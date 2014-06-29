using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kata.Data.Entities
{
	public class Ad : BaseEntity
	{
		public int NewspaperId { get; set; }

		// TODO: Fix EF6 and One-To-Many relationship 
		//public Newspaper Newspaper { get; set; } 
	}
}
