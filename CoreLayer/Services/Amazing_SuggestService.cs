using CoreLayer.DTOs;
using CoreLayer.IServices;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class Amazing_SuggestService : IAmazing_SuggestService
	{
		private readonly ShopContext _shopContext;
		public Amazing_SuggestService(ShopContext shopContext)
		{
			_shopContext = shopContext;
		}

		public async Task<OperationResault> AddSuggestasync(Amazing_Suggest suggest)
		{
			try
			{
				await _shopContext.Amazing_Suggests.AddAsync(suggest);
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public async Task<OperationResault> DeleteSuggestasync(int Id)
		{
			try
			{
				var res = await _shopContext.Amazing_Suggests.FindAsync(Id);
				_shopContext.Amazing_Suggests.Remove(res);
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public OperationResault EditSuggest(Amazing_Suggest suggest)
		{
			try
			{
				_shopContext.Amazing_Suggests.Update(suggest);
				_shopContext.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public async Task<List<ProductTitleDto>> GetActiveSuggestsasync()
		{
			List<ProductTitleDto> ResaultList = new();
			var Res = await _shopContext.Amazing_Suggests.Where(a => a.ExpireDate >= DateTime.Now).ToListAsync();
			foreach (var item in Res)
			{
				ResaultList.AddRange(_shopContext.Products.Where(p => p.Id == item.Id)
					.Include(p => p.ProductPrice)
					.Include(p => p.ProductPhotos)
					.Select(p => Mappers.ProductMapper.TOProductTitleDto(p)).AsEnumerable());
			}
			return ResaultList.ToList();
		}

		public async Task<List<Amazing_Suggest>> GetAllSuggestsasync()
		{
			var x = await _shopContext.Amazing_Suggests.Include(a=>a.Product).Include(a=>a.Product.ProductPhotos).Include(a=>a.Product.ProductPrice).ToListAsync();
			return x ;
		}

		public async Task<Amazing_Suggest> GetSuggestByIDasync(int Id)
		{
			return await _shopContext.Amazing_Suggests.FindAsync(Id);
		}
	}
}
