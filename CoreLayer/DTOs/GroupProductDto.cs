using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DTOs
{
	public class GroupProductDto
	{
		public int Id { get; set; }
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
        public string GroupProductPhoto { get; set; }
    }
}
