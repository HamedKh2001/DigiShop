using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWebCore.Areas.Admin.Models.GroupProduct;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebCore.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	[Area("Admin")]
	public class GroupProductController : Controller
	{
		#region Dependency Injection
		private readonly IGroupProductService _groupProductservice;
		private readonly IFileManager _fileManager;
		public GroupProductController(IGroupProductService productService, IFileManager fileManager)
		{
			_groupProductservice = productService;
			_fileManager = fileManager;
		}
		#endregion

		[HttpGet("Admin/GroupProduct/GroupLists/{GroupName}/{ID}")]
		public IActionResult GroupLists(string groupName, int ID)
		{
			ViewData["Title"] = $"زیر گروه های {groupName}";
			ViewData["ID"] = ID;
			var res = _groupProductservice.GetParentGroups(ID);
			return View(res);
		}

		public IActionResult Index()
		{
			return View(_groupProductservice.GetAllSuperGroups());
		}
		[HttpGet("Admin/GroupProduct/Create/{SuperParentID?}/{ParentID?}")]
		public IActionResult Create(int? superParentid, int? parentid)
		{
			return PartialView(new CreateGroupProductViewModel()
			{
				SuperParentID = superParentid,
				ParentID = parentid
			});
		}
		[HttpPost("Admin/GroupProduct/Create/{SuperParentID?}/{ParentID?}")]
		public IActionResult Create(int? superParentid, int? parentid, CreateGroupProductViewModel groupProductVM)
		{
			//if (ModelState.IsValid)
			//{
			string filename = null;
			if (groupProductVM.GroupProductPhoto != null)
			{
				filename = _fileManager.SaveFile(groupProductVM.GroupProductPhoto, FileRoots.GroupProductRoot);
			}
			var groupPVM = new GroupProductDto()
			{
				Slug = groupProductVM.Slug,
				MetaDescription = groupProductVM.MetaDescription,
				MetaTag = groupProductVM.MetaTag,
				GroupName = groupProductVM.GroupName,
				ParentID = parentid,
				GroupProductPhoto = filename,
				SuperParentID = superParentid
			};
			var res = _groupProductservice.AddGroupProduct(groupPVM);
			return Redirect("/Admin/GroupProduct");
			//}
			//return View(groupProductVM);
		}
		//[HttpGet("Admin/GroupProduct/Edit/{slug}")]
		//public IActionResult Edit(string slug)
		//{
		//	return PartialView(_groupProductservice.GetGroupBySlug(slug));
		//}
		//[HttpPost("Admin/GroupProduct/Edit/{slug}")]
		//public IActionResult Edit(string slug, GroupProductViewModel groupProductVM)
		//{
		//	_groupProductservice.EditGroupProduct(groupProductVM);
		//	return Redirect("/Admin/GroupProduct");
		//}
		[HttpGet("Admin/GroupProduct/Edit/{id}")]
		public IActionResult Edit(int id)
		{
			return PartialView(_groupProductservice.GetGroupByID(id));
		}
		[HttpPost("Admin/GroupProduct/Edit/{id}")]
		public IActionResult Edit(int id, GroupProductDto groupProductVM)
		{
			var res = _groupProductservice.EditGroupProduct(groupProductVM);
			if (res == OperationResault.Success())
			{
				return Redirect("/Admin/GroupProduct");
			}
			ModelState.AddModelError("Slug", res.Message);
			return View(groupProductVM);
		}
		[HttpGet("Admin/GroupProduct/Delete/{slug}")]
		public IActionResult Delete(string slug)
		{
			var Gp = _groupProductservice.GetGroupBySlug(slug);
			if (Gp != null)
			{
				return PartialView(Gp);
			}
			return NotFound();
		}
		[HttpPost("Admin/GroupProduct/Delete/{slug}")]
		public IActionResult Delete(string slug, GroupProductDto groupProductVM)
		{
			ViewData["title"] = "حـذف گـروه";
			var res = _groupProductservice.DeleteGroupProduct(groupProductVM.Id);
			//if (res != OperationResault.Success())
			//{
			//	ModelState.AddModelError("GroupName","عملیات ناموفق\nمحصولات این گروه را ابتدا حذف کنید");
			//	return View(groupProductVM);
			//}
			return Redirect($"Admin/GroupProduct");
		}


		[HttpGet("sort")]
		public IActionResult sort()
		{
			List<string> strs = new List<string>();
			for (int i = 0; i < 50; i++)
			{
				var x = Guid.NewGuid();
				strs.Add(x.ToString());
			}
			var linq = from n in strs
					   group n by n.Substring(0, 1).ToLower() into grp
					   select new
					   {
						   keys = grp.Key,
						   stringsingroup = grp,
						   count = grp.Count()
					   };

			List<string> res = new List<string>();

			foreach (var item in linq)
			{
				foreach (var i in item.stringsingroup)
				{
					res.Add($"{item.keys} as first letter: {i}----");
				}
			}
			return View(res);
		}

	}
}
