using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Utilities;
using CORETest.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CORETest.Pages.Auth
{
	[BindProperties]
	public class RegisterModel : PageModel
	{
		[Display(Name = "نام و نام خانوادگی")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string FullName { get; set; }

		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[MinLength(6, ErrorMessage = "{0} باید بیشتر از 5 کاراکتر باشد")]
		public string Password { get; set; }
		[Display(Name = "مجدد کلمه عبور را وارد کنید")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[Compare("Password", ErrorMessage = "رمز عبورها مطابقت ندارند")]
		public string RePassword { get; set; }
		[Display(Name = "شماره همراه")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[RegularExpression(@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$", ErrorMessage = "شماره تلفن صحیح را وارد کنید")]
		public string PhoneNumber { get; set; }
		[Display(Name = "ایمیل")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		[RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "ایمیل صحیح را وارد کنید")]
		public string Email { get; set; }
		[Display(Name = "کد ارسال شده")]
		[Required(ErrorMessage = "{0} را وارد کنید")]

		private readonly IUserService _userRegister;
		private readonly IRecaptcha _recaptcha;
		public RegisterModel(IUserService userRegister, IRecaptcha recaptcha)
		{
			_userRegister = userRegister;
			_recaptcha = recaptcha;
		}
		public void OnGet()
		{
		}

		public IActionResult OnPost()
		{
			if (ModelState.IsValid)
			{
				if (!_recaptcha.IsSatisfy().Result)
				{
					ModelState.AddModelError("PhoneNumber", "اعتبار سنجی Recaptcha انجام نشد");
					return Page();
				}
				var sms = new SMS();
				var GeneratedCode = new Random().Next(1000, 9999);
				var Res = _userRegister.SigninUsers(new AdminDto
				{
					Role = DataLayer.Entities.User.UserRole.Customer,
					FullName = FullName,
					Password = Password,
					Email = Email,
					PhoneNumber = PhoneNumber,
					ActiveCode = GeneratedCode,
					isActive = false
				});
				var smsRes = sms.sendSMS(PhoneNumber, GeneratedCode.ToString());
				if (Res.Status == OperationResultStatus.Error)
				{
					ModelState.AddModelError("UserName", Res.Message);
					return Page();
				}
				if (smsRes.Message == "عملیات با موفقیت انجام شد")
				{
					return RedirectToPage("Activation", PhoneNumber);
				}
				ModelState.AddModelError("PhoneNumber", smsRes.Message);
			}
			return Page();
		}
	}
}