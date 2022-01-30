using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreLayer.DTOs
{
	public class ProductDto:BaseEntity
	{
		public int Groupid { get; set; }
		[Display(Name = "برند")]
		public int CompanyID { get; set; }
		public int Userid { get; set; }
		[Display(Name = "نام")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string ProductName { get; set; }
		[Display(Name = "بازدید")]
		public int Visit { get; set; }
		[Display(Name = "توضیحات کامل")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Display(Name = "قـیمـت")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public int Price { get; set; }
		[Display(Name = "تصویر اسلایدر")]
		public IFormFile SliderPicture { get; set; }
		public List<IFormFile> ProductPhotoFiles { get; set; }
		[Display(Name = "رنـگها")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public List<string> ColorCode { get; set; }
		public List<int> Quantity { get; set; }
		[Display(Name = "تـخـفـیـف")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public byte Off { get; set; }
		[Display(Name = "نشان دادن در اسلایدر")]
		public bool ShowInSlider { get; set; }
		[Display(Name = "توضیح کوتاه")]
		public string ShortDescription { get; set; }
		[Display(Name = "اسلاگ")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Slug { get; set; }
		[Display(Name = "متا تگ")]
		public string MetaTag { get; set; }
		[Display(Name = "متا دیسکریپشن")]
		public string MetaDescription { get; set; }
	}
}
