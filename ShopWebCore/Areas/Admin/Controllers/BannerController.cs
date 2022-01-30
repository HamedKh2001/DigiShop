using CoreLayer.DTOs;
using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class BannerController : Controller
	{
		private readonly IBannerService _bannerService;
		private readonly IGenericRepository<Event> _genericRepository;
		public BannerController(IBannerService bannerService, IGenericRepository<Event> genericRepository)
		{
			_bannerService = bannerService;
			_genericRepository = genericRepository;
		}
		public IActionResult Index()
		{
			return View(_bannerService.GetAllBannersasync().Result);
		}

		public IActionResult Create()
		{
			var enumData = from Event.AdvertisingType e in Enum.GetValues(typeof(Event.AdvertisingType))
						   select new
						   {
							   ID = (int)e,
							   Name = e.ToString()
						   };
			//ViewData["ADsType"] = new SelectList(enumData, "ID", "Name");
			ViewBag.ADsTypes = new SelectList(enumData, "ID", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BannerDto bannerDto)
		{
			var res = _bannerService.CreateBanner(bannerDto);
			if (res.Status == OperationResultStatus.Success)
			{
				return Redirect("/Admin/Banner/");
			}
			ModelState.AddModelError("Description", res.Message);
			return View(bannerDto);
		}


		public IActionResult Edit(int Id)
		{
			var _event = _genericRepository.Get(Id);
			if (_event != null)
			{
				var enumData = from Event.AdvertisingType e in Enum.GetValues(typeof(Event.AdvertisingType))
							   select new
							   {
								   ID = (int)e,
								   Name = e.ToString()
							   };
				ViewBag.ADsTypes = new SelectList(enumData, "ID", "Name");
				return View(_event);
			}
			return NotFound();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int Id, Event @event)
		{
			_genericRepository.Update(@event);
			return Redirect("/Admin/Banner/");
		}
	}
}
