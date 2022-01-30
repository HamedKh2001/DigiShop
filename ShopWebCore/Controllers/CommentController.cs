using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWebCore.Controllers
{
	public class CommentController : Controller
	{
		#region Dependency Injection
		private readonly IProductCommentService _productCommentService;
		private readonly IRecaptcha _recaptcha;
		public CommentController(IProductCommentService productCommentService, IRecaptcha recaptcha)
		{
			_productCommentService = productCommentService;
			_recaptcha = recaptcha;
		}
		#endregion

		[HttpPost]
		public async Task<IActionResult> InsertComment(string comment, int productID)
		{
			if (_recaptcha.IsSatisfy().Result)
			{
				var res = await _productCommentService.Addcomment(new ProductcommentDto()
				{
					Comment = comment,
					ProductID = productID,
					UserID = User.GetUserID()
				});
				return new JsonResult(res.Message);
			}
			return new JsonResult("مشکل در Recaptcha!!!");
		}

		public async Task<IActionResult> GetComment(int productID)
		{
			return PartialView("_GetComment", await _productCommentService.GetCommentProduct(productID));
		}
		//("Comment/InsertComment")

	}
}
