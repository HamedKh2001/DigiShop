using CoreLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.ViewComponents
{
	public class SliderViewComponent : ViewComponent
	{
		private readonly IProductService _productService;
		public SliderViewComponent(IProductService productService)
		{
			_productService = productService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("Slider", await _productService.GetProdctuSliders());
		}
	}
}
