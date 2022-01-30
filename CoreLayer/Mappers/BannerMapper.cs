using CoreLayer.DTOs;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Mappers
{
	public static class BannerMapper
	{
		public static Event MapToEventEntity(BannerDto bannerDto)
		{
			return new Event()
			{
				Description = bannerDto.Description,
				Link = bannerDto.Link,
				ItemName = bannerDto.ItemName,
				TypeOfAdvertise=bannerDto.TypeOfAdvertise,
				IsDeleted=bannerDto.IsDeleted
			};
		}
	}
}
