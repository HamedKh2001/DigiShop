using CoreLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.ViewComponents
{
	public class HeaderViewComponent : ViewComponent
	{
		private readonly IGroupProductService _groupProductService;
		public HeaderViewComponent(IGroupProductService groupProductService)
		{
			_groupProductService = groupProductService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return await Task.FromResult(View("Header", _groupProductService.GetAllGroupsasynce()));
			//return View("Header", await _groupProductService.GetAllGroupsasynce());
		}
	}
}
