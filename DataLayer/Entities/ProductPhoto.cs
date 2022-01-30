using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class ProductPhoto : BaseEntity
	{
		public int ProductID { get; set; }
		public string PhotoName { get; set; }
		public bool IsDeleted { get; set; } = false;


		#region Relations
		[ForeignKey("ProductID")]
		public virtual Product Product { get; set; }
		#endregion
	}
}
