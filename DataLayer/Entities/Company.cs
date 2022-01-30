using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
	public class Company
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "کمپانی")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string CompanyName { get; set; }


		#region Relations
	public virtual ICollection<Product> Products { get; set; }
		#endregion	
	}
}
