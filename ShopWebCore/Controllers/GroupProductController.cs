using CoreLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Controllers
{
	[Route("GroupProduct")]
	public class GroupProductController : Controller
	{
		private readonly IProductService _productService;
		private readonly IGroupProductService _groupProductService;
		public GroupProductController(IProductService productService, IGroupProductService groupProductService)
		{
			_productService = productService;
			_groupProductService = groupProductService;
		}

		[HttpPost("{id}/{Name}")]
		public IActionResult Index(int id, string Name/*, [FromQuery(Name = "brand")] string[] brands*/)
		{
			var searchorder = HttpContext.Request.Query["searchorder"];
			//var q = brands;

			ViewData["Name"] = Name; 
			var x = _productService.GetProductByFilter(id/*, q*/, searchorder);
			return View(x);
		}

		[HttpGet("{id}/{name}")]
		public IActionResult Index(int id, string name, [FromQuery(Name = "brand")] string[] brands)
		{
			var searchorder = HttpContext.Request.QueryString.ToString();
			var searchorder1 = HttpContext.Request.QueryString.Value.ToString();
			var q = brands;

			ViewData["Name"] = name;
			var x = _productService.GetProductByFilter(id, q, searchorder);
			if (searchorder !="")
			{
				return PartialView("SearchView", x);
			}
			return View("Index", x);
		}

		[HttpGet("CategoryBox/{id}")]
		public IActionResult CategoryBox(int id)
		{
			//ViewData["CategoryName"] = Name;
			var x = _groupProductService.GetSubGroupsByID(id);
			return View(x);
		}
	}
}
