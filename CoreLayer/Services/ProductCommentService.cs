using CoreLayer.DTOs;
using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class ProductCommentService : IProductCommentService
	{
		#region Dependency Injection
		private ShopContext _shopContext;
		public ProductCommentService(ShopContext shopContext)
		{
			_shopContext = shopContext;
		}
		#endregion
		public async Task<OperationResault> Addcomment(ProductcommentDto comment)
		{
			try
			{
				_shopContext.Productcomments.Add(new Productcomment()
				{
					Comment = comment.Comment,
					UserId = comment.UserID,
					ProductId = comment.ProductID
				});
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		public async Task<List<Productcomment>> GetCommentProduct(int productID)
		{
			var res = await _shopContext.Productcomments.Where(p => p.ProductId == productID).OrderByDescending(c=>c.Id).Include(c => c.User).ToListAsync();
			return res;
		}
	}
}
