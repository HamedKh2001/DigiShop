using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class ProductsOfGroupsIIndex:BaseEntity
	{
		public int GroupId { get; set; }
		public int Qty { get; set; }


		[ForeignKey("GroupId")]
		public virtual ICollection<GroupProduct> groupProducts { get; set; }
	}
}
