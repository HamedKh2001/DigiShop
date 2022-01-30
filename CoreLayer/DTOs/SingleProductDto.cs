using DataLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreLayer.DTOs
{
	public class SingleProductDto
	{
		public int ID { get; set; }
		public string GroupName { get; set; }
		public string ShopName { get; set; }
		[Display(Name = "نام")]
		public string ProductName { get; set; }
		[Display(Name = "بازدید")]
		public int Visit { get; set; }
		[Display(Name = "توضیحات کامل")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Display(Name = "قـیمـت")]
		public int Price { get; set; }
		[Display(Name = "تصویر اسلایدر")]
		public string SliderPicture { get; set; }
		public List<ProductPhoto> ProductPhotos { get; set; }
		[Display(Name = "رنـگها")]
		public List<ProductColor> Color { get; set; }
		[Display(Name = "تـخـفـیـف")]
		public int Off { get; set; }
		[Display(Name = "کامنتها")]
		[DataType(DataType.MultilineText)]
		public bool ShowInSlider { get; set; }
		[Display(Name = "توضیح کوتاه")]
		[DataType(DataType.MultilineText)]
		public string ShortDescription { get; set; }
		[Display(Name = "برند")]
		public Company Company { get; set; }
		[Display(Name = "اسلاگ")]
		public string Slug { get; set; }

	}
}
