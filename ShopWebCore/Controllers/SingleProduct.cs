using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Mappers;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Controllers
{
	[Route("/SingleProduct")]
	public class SingleProduct : Controller
	{
		private readonly IProductService _productService;
		private readonly IProductCommentService _productCommentService;
		public SingleProduct(IProductService productService,IProductCommentService productCommentService)
		{
			_productService = productService;
			_productCommentService = productCommentService;
		}
		[HttpGet("{id}/{slug}")]
		public IActionResult Index(int id, string slug)
		{
			ViewData["Slug"] = slug;
			var x = ProductMapper.MaptoSingleProductDto(_productService.GetProductByID(id));
			x.ID = id;
			return View(x);
		}
		
	}
}
