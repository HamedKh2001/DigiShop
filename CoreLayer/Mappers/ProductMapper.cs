using CoreLayer.DTOs;
using DataLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLayer.Mappers
{
	public static class ProductMapper
	{
		public static Product MaptoProduct(ProductDto p)
		{
			return new Product()
			{
				CompanyId = p.CompanyID,
				Description = p.Description,
				Groupid = p.Groupid,
				MetaDescription = p.MetaDescription,
				MetaTag = p.MetaTag,
				Off = p.Off,
				ProductName = p.ProductName,
				ShortDescription = p.ShortDescription,
				ShowInSlider = p.ShowInSlider,
				Slug = p.Slug,
				Userid = p.Userid,
				Visit = p.Visit
			};
		}
		public static ProductTitleDto TOProductTitleDto(Product product)
		{
			var listphoto = new List<string>();
			if (product.ProductPhotos !=null)
			{
				foreach (var item in product.ProductPhotos)
				{
					listphoto.Add(item.PhotoName);
				}
			}
			return new ProductTitleDto()
			{
				id = product.Id,
				Off = product.Off,
				Price = product.ProductPrice.Price,
				ProductName = product.ProductName,
				ShortDescription = product.ShortDescription,
				ProductPhotos = listphoto,
				SliderPhoto = product.SliderPicture,
				Slug = product.Slug,
				ShopName=product.Admin.FullName,
				CompanyId= product.CompanyId,
				Groupid= product.Groupid
			};
		}
		public static SingleProductDto MaptoSingleProductDto(Task<Product> product)
		{
			var p = product.Result;
			return new SingleProductDto
			{
				GroupName = p.GroupProduct.GroupName,
				Color = p.ProductColors.ToList(),
				Company = p.Company,
				Description = p.Description,
				Off = p.Off,
				Price = p.ProductPrice.Price,
				ProductName = p.ProductName,
				ProductPhotos = p.ProductPhotos.ToList(),
				ShopName = p.Admin.FullName,
				ShortDescription = p.ShortDescription,
				ShowInSlider = p.ShowInSlider,
				SliderPicture = p.SliderPicture,
				Slug = p.Slug,
				Visit = p.Visit
			};
		}
		public static Product MapEditProductDtotoProduct(EditProductDto product)
		{
			return new Product
			{
				Id = product.Id,
				CompanyId = product.CompanyID,
				Description = product.Description,
				Groupid = product.Group.Id,
				MetaDescription = product.MetaDescription,
				MetaTag = product.MetaTag,
				Off = product.Off,
				ProductName = product.ProductName,
				Slug = product.Slug,
				ShowInSlider = product.ShowInSlider,
				ShortDescription = product.ShortDescription,
				
			};
		}
	}
}
