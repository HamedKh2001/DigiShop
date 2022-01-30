using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Productcomment: BaseEntity
	{
		[Required]
		public string Comment { get; set; }
		[Required]
		public int UserId { get; set; }
		[Required]
		public int ProductId { get; set; }
		public bool IsDeleted { get; set; } = false;


		#region Relations
		[ForeignKey("UserId")]
		public virtual User User { get; set; }
		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
		#endregion
	}
}
