using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreLayer.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CORETest.Pages.Auth
{
	[BindProperties]
	[ValidateAntiForgeryToken]
	public class LoginModel : PageModel
	{
		[Display(Name = "نام کاربری")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string PhoneNumber { get; set; }
		[Display(Name = "کلمه عبور")]
		[Required(ErrorMessage = "{0} را وارد کنید")]
		public string Password { get; set; }
		public bool RememberMe { get; set; }

		private readonly IUserService _userServices;
		public LoginModel(IUserService userServices)
		{
			_userServices = userServices;
		}

		public void OnGet()
		{

		}

		public IActionResult OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			var resault = _userServices.LoginUsers(new CoreLayer.DTOs.AdminDto
			{
				Password = Password,
				PhoneNumber = PhoneNumber,
			});
			if (resault == null)
			{
				ModelState.AddModelError("PhoneNumber", "نام کاربری یا رمز عبور اشتباه است ");
				return Page();
			}
			List<Claim> Claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,resault.FullName),
				new Claim(ClaimTypes.NameIdentifier,resault.Id.ToString()),
				new Claim(ClaimTypes.Role,resault.Role.ToString())
			};
			var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var Claimprincipal = new ClaimsPrincipal(Identity);
			var prop = new AuthenticationProperties()
			{
				IsPersistent = RememberMe
			};
			HttpContext.SignInAsync(Claimprincipal, prop);
			var x = HttpContext.Request.Query["ReturnUrl"];
			if (x.Count != 0)
			{
				return Redirect(x);
			}
			return Redirect("/");
		}
	}
}
