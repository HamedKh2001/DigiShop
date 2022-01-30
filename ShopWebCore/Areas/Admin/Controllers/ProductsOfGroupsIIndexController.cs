using CoreLayer.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopWebCore.Areas.Admin.Models.ProductsOfGroupsIIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ProductsOfGroupsIIndexController : Controller
	{
		private readonly IGenericRepository<DataLayer.Entities.GroupProduct> _GPgenericRepository;
		private readonly IGenericRepository<DataLayer.Entities.ProductsOfGroupsIIndex> _GIgenericRepository;
		public ProductsOfGroupsIIndexController(IGenericRepository<DataLayer.Entities.GroupProduct> GPgenericRepository,
			IGenericRepository<DataLayer.Entities.ProductsOfGroupsIIndex> GIgenericRepository)
		{
			_GPgenericRepository = GPgenericRepository;
			_GIgenericRepository = GIgenericRepository;
		}
		public IActionResult Index()
		{
			return View(_GIgenericRepository.GetAll().ToList());
		}

		public IActionResult Create()
		{
			ViewBag.GroupList = new SelectList(_GPgenericRepository.GetAll().ToList(), "Id", "GroupName");
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateProductsOfGroupsIIndexViewModel createProductsOfGroupsIIndexVM)
		{
			foreach (var item in createProductsOfGroupsIIndexVM.GroupIdList)
			{
				_GIgenericRepository.Insert(new DataLayer.Entities.ProductsOfGroupsIIndex()
				{
					Qty = createProductsOfGroupsIIndexVM.Qty,
					GroupId = item
				});
			}
			return Redirect("/Admin");
		}

		public IActionResult Delete(int Id)
		{
			if (_GIgenericRepository.Delete(_GIgenericRepository.Get(Id)))
			{
				return new JsonResult("عملیات با موفقیت انجام شد");
			}
			return View();
		}

	}
}
