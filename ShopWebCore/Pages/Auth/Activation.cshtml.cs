using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CoreLayer.IServices;
using CoreLayer.Utilities;
using CORETest.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShopWebCore.Pages.Auth
{
	public class ActivationModel : PageModel
	{
		private IUserService _userService;
		public ActivationModel(IUserService userService)
		{
			_userService = userService;
		}

		[BindProperty]
		[Display(Name = "کد ارسال شده در پیامک را وارد کنید")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public int EnteredCode { get; set; }

		public IActionResult OnGet([FromQuery(Name = "handler")] string PhoneNumber)
		{
			var x = HttpContext.Request.Path.Value.Split("/")[3];
			return Page();
		}
		public IActionResult OnPost()
		{
			var PhoneNumber = HttpContext.Request.Path.Value.Split("/")[3];
			if (EnteredCode == 0)
			{
				return BadRequest();
			}
			var user = _userService.GetUser(PhoneNumber);
			if (user.ActiveCode == EnteredCode)
			{
				user.IsActive = true;
				var res = _userService.ActivateUser(user);
				if (res.Message == "عملیات با موفقیت انجام شد")
				{
					return Redirect("/");
				}
				ModelState.AddModelError("EnteredCode", res.Message);
				return Page();
			}
			ModelState.AddModelError("EnteredCode", "کد وارد شده اشتباه است");
			return Page();
		}

		public IActionResult OnGetReSendSMS(string PhoneNumber)
		{
			var user = _userService.GetUser(PhoneNumber);
			var sms = new SMS();
			var res = sms.sendSMS(PhoneNumber, user.ActiveCode.ToString());
			if (res.Message == "عملیات با موفقیت انجام شد")
			{
				return new JsonResult("ارسال پیامک موفقیت آمیز بود");
			}
			return new JsonResult("خطا در ارسال پیامک!!!");
		}
	}
}
