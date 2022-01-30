using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Pages.Auth
{
	
	public class Logout : Controller
	{
		[Route("Logout")]
		public IActionResult LogoutUser()
		{
			HttpContext.SignOutAsync();
			return Redirect("/");
		}
	}
}
