using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class ProductPrice:BaseEntity
	{
		public int Price { get; set; }
		public int ProductID { get; set; }


		#region Relations
		public virtual Product Product { get; set; }
		#endregion
	}
}
