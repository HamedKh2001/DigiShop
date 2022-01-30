using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopWebCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class Amazing_SuggestController : Controller
	{
		private readonly IAmazing_SuggestService _amazing_SuggestService;
		private readonly IProductService _productService;
		public Amazing_SuggestController(IAmazing_SuggestService amazing_SuggestService, IProductService productService)
		{
			_amazing_SuggestService = amazing_SuggestService;
			_productService = productService;
		}
		public IActionResult Index()
		{
			return View(_amazing_SuggestService.GetAllSuggestsasync().Result);
		}

		public IActionResult Create()
		{
			ViewBag.ProductList = new SelectList(_productService.GetAllProducts().Result, "Id", "ProductName");
			return View();
		}

		[HttpPost]
		public IActionResult Create(Amazing_Suggest suggest)
		{
			if (ModelState.IsValid)
			{
				var res = _amazing_SuggestService.AddSuggestasync(suggest).Result;
				if (res.Status != OperationResultStatus.Success)
				{
					ModelState.AddModelError("ProductId", res.Message);
					return View();
				}
				return Redirect("/admin/Amazing_Suggest");
			}
			return View();
		}

		public IActionResult Edit(int Id)
		{
			ViewBag.ProductList = new SelectList(_productService.GetAllProducts().Result, "Id", "ProductName");
			return View(_amazing_SuggestService.GetSuggestByIDasync(Id).Result);
		}

		[HttpPost]
		public IActionResult Edit(Amazing_Suggest suggest)
		{
			if (ModelState.IsValid)
			{
				var res = _amazing_SuggestService.EditSuggest(suggest);
				if (res.Status != OperationResultStatus.Success)
				{
					ModelState.AddModelError("ProductId", res.Message);
					return View();
				}
				return Redirect("/Admin/Amazing_Suggest");
			}
			return View();
		}

		public IActionResult Delete(int Id)
		{
			var Res = _amazing_SuggestService.DeleteSuggestasync(Id).Result;
			return new JsonResult(Res.Status);
		}
	}
}
