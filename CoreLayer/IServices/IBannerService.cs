using CoreLayer.DTOs;
using CORETest.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IBannerService
	{
		OperationResault CreateBanner(BannerDto bannerDto);
		Task<List<Event>> GetAllBannersasync();
		Task<Event> GetEventasync(int Id);
	}
}
