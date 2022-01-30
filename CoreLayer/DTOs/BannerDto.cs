using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Entities.Event;

namespace CoreLayer.DTOs
{
	public class BannerDto:BaseEntity
	{
		public int EventID { get; set; }
		[Display(Name = "تصویر بنر")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public IFormFile PhotoName { get; set; }
		[Display(Name = "غیرفعال کردن")]
		public bool IsDeleted { get; set; }
		[Display(Name = "عنوان تبلیغ")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string ItemName { get; set; }
		[Display(Name = "توضیحات")]
		public string Description { get; set; }
		[Display(Name = "لینک")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Link { get; set; }
		[Display(Name = "نوع بنر")]
		public AdvertisingType TypeOfAdvertise { get; set; }
	}
}
