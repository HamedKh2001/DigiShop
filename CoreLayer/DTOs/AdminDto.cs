using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataLayer.Entities.User;

namespace CoreLayer.DTOs
{
	public class AdminDto 
	{
		public int Id { get; set; }
		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string FullName { get; set; }
		[Display(Name = "رمز عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Display(Name = "تکرار رمز عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage ="رمز عبور مطابقت ندارد")]
		public string RePassword { get; set; }
		public UserRole Role { get; set; }
		[Display(Name = "شماره همراه")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.PhoneNumber,ErrorMessage = "شماره تلفن صحیح را وارد کنید")]
		public string PhoneNumber { get; set; }
		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[DataType(DataType.EmailAddress, ErrorMessage = "ایمیل صحیح را وارد کنید")]
		public string Email { get; set; }
		public int ActiveCode { get; set; }
		public bool isActive { get; set; }

	}
}
