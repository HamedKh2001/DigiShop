using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace DataLayer.Entities
{
	[Serializable]
	public class Product : BaseEntity
	{
		public int Groupid { get; set; }
		public int Userid { get; set; }
		public int CompanyId { get; set; }
		[Display(Name = "نام")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string ProductName { get; set; }
		[Display(Name = "بازدید")]
		public int Visit { get; set; } = 0;
		[Display(Name = "تصویر اسلایدر")]
		public string SliderPicture { get; set; }
		[Display(Name = "توضیحات کامل")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		[Display(Name = "تـخـفـیـف")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public Byte Off { get; set; }
		[Display(Name = "نشان دادن در اسلایدر")]
		public bool ShowInSlider { get; set; }
		[Display(Name = "توضیح کوتاه")]
		[DataType(DataType.MultilineText)]
		public string ShortDescription { get; set; }
		[Display(Name = "اسلاگ")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Slug { get; set; }
		[Display(Name = "متا تگ")]
		public string MetaTag { get; set; }
		[Display(Name = "متا دیسکریپشن")]
		public string MetaDescription { get; set; }
		[Display(Name = "تایید شده ")]
		public bool IsApprove { get; set; } = false;

		#region Relations
		[ForeignKey("CompanyId")]
		public virtual Company Company { get; set; }
		[XmlIgnore]
		[ForeignKey("Groupid")]
		public virtual GroupProduct GroupProduct { get; set; }
		[XmlIgnore]
		public virtual ICollection<Productcomment> Productcomments { get; set; }
		[XmlIgnore]
		public virtual ICollection<ProductColor> ProductColors { get; set; }
		[XmlIgnore]
		[ForeignKey("Userid")]
		public virtual User Admin { get; set; }
		[XmlIgnore]
		public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
		public virtual ProductPrice ProductPrice { get; set; }
		public virtual Amazing_Suggest Amazing_Suggest { get; set; }
		#endregion


	}
}
