using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class OrderDetail: BaseEntity
	{
		public int OrderId { get; set; }
		public Product Product { get; set; }
		public int Quantity { get; set; }
		public OrderDetail()
		{

		}


		#region Relations
		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }
		#endregion

	}
}
