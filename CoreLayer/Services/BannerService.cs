using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Mappers;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
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
	public class BannerService : IBannerService
	{
		private readonly ShopContext _shopContext;
		private readonly IFileManager _fileManager;
		public BannerService(ShopContext shopContext, IFileManager fileManager)
		{
			_shopContext = shopContext;
			_fileManager = fileManager;
		}

		public OperationResault CreateBanner(BannerDto bannerDto)
		{
			try
			{
				var picName = _fileManager.SaveFile(bannerDto.PhotoName, FileRoots.BannerRoot);
				var res = _shopContext.Events.Add(BannerMapper.MapToEventEntity(bannerDto));
				_shopContext.SaveChanges();
				_shopContext.BannerPhotos.Add(new BannerPhoto()
				{
					PhotoName = picName,
					EventID = res.Entity.Id
				});
				_shopContext.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public async Task<List<Event>> GetAllBannersasync()
		{
			return await _shopContext.Events
				.Include(e => e.BannerPhotos)
				.ToListAsync();
		}
		public async Task<Event> GetEventasync(int Id)
		{
			return await _shopContext.Events.Include(e => e.BannerPhotos).FirstOrDefaultAsync(e=>e.Id==Id);
		}
	}
}
