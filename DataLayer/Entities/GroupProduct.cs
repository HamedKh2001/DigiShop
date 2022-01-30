using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class GroupProduct : BaseEntity
	{
		[Required]
		public string GroupName { get; set; }
		[Required]
		public string Slug { get; set; }
		public string MetaTag { get; set; }
		public string MetaDescription { get; set; }
		public int? ParentID { get; set; }
		public int? SuperParentID { get; set; }
		public string GroupProductPhoto { get; set; }


		#region Relations
		public virtual ICollection<Product> Products { get; set; }
		public ProductsOfGroupsIIndex productsOfGroupsIIndex { get; set; }
		#endregion
	}
}
