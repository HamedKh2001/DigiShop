using CoreLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Controllers
{
	
	public class HomeController : Controller
	{
		public HomeController()
		{
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet("404")]
		public IActionResult _404()
		{
			return View("_404");
		}

	}
}
