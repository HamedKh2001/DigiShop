using CoreLayer.DTOs;
using DataLayer.Entities;

namespace CoreLayer.Mappers
{
	public static class GroupProductMapper
	{
		public static GroupProduct MapToGroupProduct(GroupProductDto groupProduct)
		{
			return new GroupProduct()
			{
				Slug = groupProduct.Slug,
				MetaDescription = groupProduct.MetaDescription,
				MetaTag = groupProduct.MetaTag,
				GroupName = groupProduct.GroupName,
				SuperParentID = groupProduct.SuperParentID,
				ParentID = groupProduct.ParentID,
				GroupProductPhoto = groupProduct.GroupProductPhoto
			};
		}
		public static GroupProductDto MapToGroupProductDto(GroupProduct groupProduct)
		{
			if (groupProduct == null)
			{
				return null;
			}
			return new GroupProductDto()
			{
				Id = groupProduct.Id,
				Slug = groupProduct.Slug,
				MetaDescription = groupProduct.MetaDescription,
				MetaTag = groupProduct.MetaTag,
				GroupName = groupProduct.GroupName,
				SuperParentID = groupProduct.SuperParentID,
				ParentID = groupProduct.ParentID,
				GroupProductPhoto = groupProduct.GroupProductPhoto
			};
		}
	}
}
