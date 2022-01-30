using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Utilities;
using CORETest.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace ShopWebCore.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;
		private readonly IGroupProductService _groupProductService;
		private readonly ICompanyService _CompanyService;
		public ProductsController(IGroupProductService groupProductService, IProductService productService, ICompanyService companyService)
		{
			_productService = productService;
			_groupProductService = groupProductService;
			_CompanyService = companyService;
		}

		public async Task<IActionResult> Index()
		{
			var shopContext = await _productService.GetAllProducts();
			return View(shopContext);
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var product = await _productService.GetProductByID(id.Value);
			if (product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		public IActionResult Create()
		{
			ViewData["Groupid"] = new SelectList(_groupProductService.GetAllGroups(), "Id", "GroupName");
			ViewData["Companyid"] = new SelectList(_CompanyService.GetCompanyList().Result, "Id", "CompanyName");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductDto productdto)
		{
			if (ModelState.IsValid)
			{
				var ax = User.GetUserID();
				productdto.Userid = ax;
				var res = await _productService.InsertProduct(productdto);
				if (res.Status == OperationResultStatus.Success)
				{
					return Redirect("/Admin/products");
				}
				ModelState.AddModelError("ProductName", res.Message);
			}
			ViewData["Groupid"] = new SelectList(_groupProductService.GetAllGroups(), "Id", "GroupName");
			ViewData["Companyid"] = new SelectList(_CompanyService.GetCompanyList().Result, "Id", "CompanyName");
			return View();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _productService.GetProductByIDForEdit(id.Value);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["Groupid"] = new SelectList(_groupProductService.GetAllGroups(), "Id", "GroupName", product.Group.Id);
			ViewData["Companyid"] = new SelectList(_CompanyService.GetCompanyList().Result, "Id", "CompanyName", product.Company.Id);
			return View(product);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, EditProductDto product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					await _productService.EditProduct(product);
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!_productService.ProductExists(product.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			ViewData["Groupid"] = new SelectList(_groupProductService.GetAllGroups(), "Id", "GroupName", product.Group.Id);
			ViewData["Companyid"] = new SelectList(_CompanyService.GetCompanyList().Result, "Id", "CompanyName", product.Company.Id);
			return View(product);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var product = await _productService.GetProductByID(id.Value);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var res = await _productService.DeleteProductByID(id);
			return RedirectToAction(nameof(Index));
			//ModelState.AddModelError("ProductName", "عملیات با موفقیت انجام نشد");
			//return View();
		}

		public async Task<IActionResult> DeleteColor(ProductColor color)
		{
			if (color == null)
				return NotFound();
			var res = await _productService.DeleteProductColor(color);
			return View(res);
		}
		//[HttpGet("Admin/Products/DeletePhoto/{name}")]
		public async Task<IActionResult> DeletePhoto(string name)
		{
			var res = await _productService.DeleteProductPhoto(name);
			return new JsonResult(res);
		}
		
	}
}