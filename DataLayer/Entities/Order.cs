using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Order : BaseEntity
	{
		List<OrderDetail> _orderDetail = new List<OrderDetail>();
		public Order(List<OrderDetail> orderDetail)
		{
			_orderDetail = orderDetail;
		}
		public Order()
		{

		}
		public int CustomerId { get; set; }

		public int Total
		{
			get
			{
				int totalprice = 0;
				foreach (var item in _orderDetail)
				{
					totalprice += (item.Product.ProductPrice.Price) * item.Quantity;
				}
				return totalprice;
			}
			set { }
		}

		#region Relations
		[ForeignKey("CustomerId")]
		public virtual User User { get; set; }
		[ForeignKey("Id")]
		public virtual OrderDetail OrderDetail { get; set; }
		#endregion

	}
}
