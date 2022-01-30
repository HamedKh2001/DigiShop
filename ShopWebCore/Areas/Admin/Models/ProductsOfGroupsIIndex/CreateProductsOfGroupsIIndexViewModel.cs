using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebCore.Areas.Admin.Models.ProductsOfGroupsIIndex
{
	public class CreateProductsOfGroupsIIndexViewModel
	{
		public List<int> GroupIdList { get; set; }
		[Display(Name ="تعداد محصول در هر ردیف")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public int Qty { get; set; }
	}
}
