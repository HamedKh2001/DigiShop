using CoreLayer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class HomeController : Controller
	{
		readonly private IUserService _userService;
		public HomeController(IUserService userService)
		{
			_userService = userService;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AllUsers()
		{
			return View(_userService.GetAllUsers());
		}
	}
}
