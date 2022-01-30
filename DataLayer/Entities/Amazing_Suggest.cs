using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Amazing_Suggest:BaseEntity
	{
		[Required]
		[Display(Name ="نام محصول")]
		public int ProductId { get; set; }
		[Display(Name ="تاریخ انقضا")]
		public DateTime ExpireDate { get; set; }
		[Display(Name ="فعال")]
		public bool IsActive { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
	}
}
