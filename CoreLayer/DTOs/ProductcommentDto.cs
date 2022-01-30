using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
	public class ProductcommentDto
	{
		[Display(Name = "نظر شما")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Comment { get; set; }

		public int UserID { get; set; }

		public int ProductID { get; set; }
	}
}
