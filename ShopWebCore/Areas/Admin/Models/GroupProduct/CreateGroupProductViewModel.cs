using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Areas.Admin.Models.GroupProduct
{
	public class CreateGroupProductViewModel
	{
        [Display(Name = "نام گـروه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string GroupName { get; set; }
        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Slug { get; set; }
        [Display(Name = "متا تگ")]
        public string MetaTag { get; set; }
        [Display(Name = "متا دیسکریپشن")]
        public string MetaDescription { get; set; }
        [Display(Name = "نام زیر گروه")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public int? ParentID { get; set; }
		public int? SuperParentID { get; set; }
        [Display(Name = "تصویر منو")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public IFormFile GroupProductPhoto { get; set; }
	}
}
