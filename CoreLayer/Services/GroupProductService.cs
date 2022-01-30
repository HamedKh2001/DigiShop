using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Mappers;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using DataLayer;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
	public class GroupProductService : IGroupProductService
	{
		private ShopContext _shopContext;
		private readonly IFileManager _fileManager;
		public GroupProductService(ShopContext shopContext, IFileManager fileManager)
		{
			_shopContext = shopContext;
			_fileManager = fileManager;
		}

		public OperationResault AddGroupProduct(GroupProductDto groupProductDto)
		{
			try
			{
				groupProductDto.Slug = groupProductDto.Slug.ToSlug();
				var Res = GroupProductMapper.MapToGroupProduct(groupProductDto);
				_shopContext.GroupProducts.Add(Res);
				_shopContext.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		public OperationResault DeleteGroupProduct(int Id)
		{
			try
			{
				var category = _shopContext.GroupProducts.FirstOrDefault(g => g.Id == Id);
				_shopContext.GroupProducts.Remove(category);
				_shopContext.SaveChanges();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.ToString());
			}
		}
		public OperationResault EditGroupProduct(GroupProductDto groupProductDto)
		{
			bool isexistslug = _shopContext.GroupProducts.Any(g => g.Slug == groupProductDto.Slug && g.Id != groupProductDto.Id);
			if (isexistslug)
			{
				return OperationResault.Error("Slug موجود میباشد");
			}
			else
			{
				var GP = _shopContext.GroupProducts.SingleOrDefault(g => g.Id == groupProductDto.Id);
				if (groupProductDto.ParentID != null)
					GP.ParentID = groupProductDto.ParentID;
				if (groupProductDto.ParentID == null)
					GP.ParentID = 0;
				GP.GroupName = groupProductDto.GroupName;
				GP.MetaTag = groupProductDto.MetaTag;
				GP.MetaDescription = groupProductDto.MetaDescription;
				GP.Slug = groupProductDto.Slug;
				_shopContext.GroupProducts.Update(GP);
				_shopContext.SaveChanges();
				return OperationResault.Success();
			}
		}
		public IEnumerable<GroupProductDto> GetAllParentGroups()
		{
			return _shopContext.GroupProducts.Where(g => g.ParentID == 0 && g.SuperParentID != null).Select(g => new GroupProductDto()
			{
				GroupName = g.GroupName,
				MetaDescription = g.MetaDescription,
				MetaTag = g.MetaTag,
				Id = g.Id,
				ParentID = g.ParentID,
				Slug = g.Slug
			}).ToList();
		}
		public IEnumerable<GroupProductDto> GetParentGroups(int Id)
		{
			return _shopContext.GroupProducts.Where(g => (g.ParentID == null && g.SuperParentID == Id) || (g.ParentID == Id)).Select(g => new GroupProductDto()
			{
				GroupName = g.GroupName,
				MetaDescription = g.MetaDescription,
				MetaTag = g.MetaTag,
				Id = g.Id,
				ParentID = g.ParentID,
				Slug = g.Slug,
				SuperParentID = g.SuperParentID
			}).ToList();
		}
		public GroupProductDto GetGroupByID(int Id)
		{
			var Gp = _shopContext.GroupProducts.SingleOrDefault(g => g.Id == Id);
			if (Gp != null)
			{
				return new GroupProductDto()
				{
					Id = Gp.Id,
					GroupName = Gp.GroupName,
					MetaDescription = Gp.MetaDescription,
					MetaTag = Gp.MetaTag,
					ParentID = Gp.ParentID,
					Slug = Gp.Slug
				};
			}
			return null;
		}
		public GroupProductDto GetGroupBySlug(string slug)
		{
			var Gp = _shopContext.GroupProducts.SingleOrDefault(g => g.Slug == slug);
			if (Gp != null)
			{
				return new GroupProductDto()
				{
					Id = Gp.Id,
					GroupName = Gp.GroupName,
					MetaDescription = Gp.MetaDescription,
					MetaTag = Gp.MetaTag,
					ParentID = Gp.ParentID,
					Slug = Gp.Slug
				};
			}
			return null;
		}
		public IEnumerable<GroupProductDto> GetGroupsByParentId(int Id)
		{
			return _shopContext.GroupProducts.Where(g => g.ParentID == Id).Select(g => new GroupProductDto()
			{
				GroupName = g.GroupName,
				MetaDescription = g.MetaDescription,
				MetaTag = g.MetaTag,
				Id = g.Id,
				ParentID = g.ParentID,
				Slug = g.Slug
			}).ToList();
		}
		public IEnumerable<GroupProductDto> GetAllchildGroups()
		{
			return _shopContext.GroupProducts.Where(g => g.ParentID != 0).Select(g => new GroupProductDto()
			{
				GroupName = g.GroupName,
				MetaDescription = g.MetaDescription,
				MetaTag = g.MetaTag,
				Id = g.Id,
				ParentID = g.ParentID,
				Slug = g.Slug
			}).ToList();
		}
		public async Task<IEnumerable<GroupProductDto>> GetAllGroupsasynce()
		{
			return await _shopContext.GroupProducts.Select(p => GroupProductMapper.MapToGroupProductDto(p)).ToListAsync();
		}
		public IEnumerable<GroupProductDto> GetAllGroups()
		{
			return _shopContext.GroupProducts.Select(g => new GroupProductDto()
			{
				GroupName = g.GroupName,
				MetaDescription = g.MetaDescription,
				MetaTag = g.MetaTag,
				Id = g.Id,
				ParentID = g.ParentID,
				Slug = g.Slug,
				SuperParentID = g.SuperParentID,
				GroupProductPhoto = g.GroupProductPhoto
			}).ToList();
		}
		public IEnumerable<GroupProductDto> GetAllSuperGroups()
		{
			return _shopContext.GroupProducts.Where(g => g.SuperParentID == null).Select(g => new GroupProductDto()
			{
				GroupName = g.GroupName,
				MetaDescription = g.MetaDescription,
				MetaTag = g.MetaTag,
				Id = g.Id,
				ParentID = g.ParentID,
				Slug = g.Slug
			}).ToList();
		}
		public async Task<IEnumerable<GroupProductDto>> GetSubGroupsByID(int Id)
		{
			var target = await _shopContext.GroupProducts.FirstAsync(g => g.Id == Id);
			if (target.SuperParentID == null && target.ParentID == null)
			{
				return await _shopContext.GroupProducts
					.Where(g => g.SuperParentID == target.Id)
					.Select(g => new GroupProductDto()
					{
						GroupName = g.GroupName,
						MetaDescription = g.MetaDescription,
						MetaTag = g.MetaTag,
						Id = g.Id,
						ParentID = g.ParentID,
						Slug = g.Slug
					}).AsNoTracking().ToListAsync();
			}
			if (target.SuperParentID != null && target.ParentID == null)
			{
				return await _shopContext.GroupProducts
				.Where(g => g.ParentID == target.Id)
				.Select(g => new GroupProductDto()
				{
					GroupName = g.GroupName,
					MetaDescription = g.MetaDescription,
					MetaTag = g.MetaTag,
					Id = g.Id,
					ParentID = g.ParentID,
					Slug = g.Slug
				}).AsNoTracking().ToListAsync();
			}
			return null;
		}
	}
}
