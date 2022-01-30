using CoreLayer.DTOs;
using CoreLayer.IServices;
using CoreLayer.Mappers;
using CoreLayer.Services.FileManager;
using CoreLayer.Utilities;
using CORETest.Utilities;
using DataLayer.Context;
using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CoreLayer.Services
{
	public class ProductService : IProductService
	{
		#region Dependency Injection
		private readonly ShopContext _shopContext;
		private readonly IFileManager _fileManager;
		public ProductService(ShopContext shopContext, IFileManager fileManager)
		{
			_shopContext = shopContext;
			_fileManager = fileManager;
		}
		#endregion
		private bool InsertProductColors(ProductDto productDto, int productiId)
		{
			try
			{
				for (int i = 0; i < productDto.ColorCode.Count; i++)
				{
					_shopContext.ProductColors.Add(new ProductColor
					{
						ColorCode = productDto.ColorCode[i],
						ProductId = productiId,
						Quantity = productDto.Quantity[i]
					});
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		private bool InsertProductPhotoList(ProductDto productDto, int productiId)
		{
			try
			{
				var filenames = _fileManager.SaveListFile(productDto.ProductPhotoFiles, FileRoots.SliderRoot);
				foreach (var item in filenames)
				{
					_shopContext.ProductPhotos.Add(new ProductPhoto()
					{
						PhotoName = item,
						ProductID = productiId
					});
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		private async Task<OperationResault> DeleteProductPhotoFromDB(string photo)
		{
			try
			{
				_shopContext.ProductPhotos.Remove(await _shopContext.ProductPhotos.FirstOrDefaultAsync(p => p.PhotoName == photo));
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		private async Task<OperationResault> InsertPrice(int id, int price)
		{
			try
			{
				await _shopContext.ProductPrices.AddAsync(new ProductPrice()
				{
					Price = price,
					ProductID = id
				});
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}

		public async Task<OperationResault> DeleteCompany(int Id)
		{
			try
			{
				var company = await _shopContext.Companies.FirstOrDefaultAsync(c => c.Id == Id);
				_shopContext.Companies.Remove(company);
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		public async Task<OperationResault> DeleteProductColor(ProductColor color)
		{
			try
			{
				_shopContext.ProductColors.Remove(color);
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		public async Task<OperationResault> DeleteProductPhoto(string photo)
		{
			try
			{
				var res = await DeleteProductPhotoFromDB(photo);
				if (res == OperationResault.Success())
				{
					_fileManager.DeleteFile(Directory.GetCurrentDirectory() + FileRoots.GetStreamRoot, photo);
					return OperationResault.Success();
				}
				return res;
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		public async Task<OperationResault> InsertProduct(ProductDto productDto)
		{
			try
			{
				productDto.Slug = productDto.Slug.ToSlug();
				if (_shopContext.Products.Any(p => p.Slug == productDto.Slug))
					return OperationResault.Error("اسلاگ موجود میباشد");

				var product = ProductMapper.MaptoProduct(productDto);
				if (productDto.SliderPicture != null)
				{
					var slidrename = _fileManager.SaveFile(productDto.SliderPicture, FileRoots.SliderRoot);
					product.SliderPicture = slidrename;
				}
				_shopContext.Products.Add(product);
				await _shopContext.SaveChangesAsync();
				InsertProductColors(productDto, product.Id);
				await InsertPrice(product.Id, productDto.Price);
				if (productDto.ProductPhotoFiles.Count != 0)
					InsertProductPhotoList(productDto, product.Id);
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
		public async Task<OperationResault> DeleteProductByID(int id)
		{
			try
			{
				var product = await _shopContext.Products.Include(p => p.ProductPhotos).Include(p => p.ProductColors).FirstOrDefaultAsync(p => p.Id == id);

				if (product.SliderPicture != null)
				{
					File.Delete(Directory.GetCurrentDirectory() + FileRoots.GetStreamRoot + product.SliderPicture);
				}
				if (product.ProductPhotos.Count != 0)
				{
					foreach (var item in product.ProductPhotos)
					{
						_fileManager.DeleteFile(Directory.GetCurrentDirectory() + FileRoots.GetStreamRoot, item.PhotoName);
						_shopContext.ProductPhotos.Remove(item);
					}
				}
				if (product.ProductColors.Count != 0)
				{
					foreach (var item in product.ProductColors)
					{
						_shopContext.ProductColors.Remove(item);
					}
				}
				_shopContext.Products.Remove(product);
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.ToString());
			}
		}
		public async Task<OperationResault> EditProduct(EditProductDto product)
		{
			try
			{
				_shopContext.Products.Update(ProductMapper.MapEditProductDtotoProduct(product));
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.ToString());
			}
		}
		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			try
			{
				var product = await _shopContext.Products
					.Include(p => p.GroupProduct)
					.Include(p => p.Admin)
					.Include(p => p.ProductPhotos)
					.Include(p => p.ProductColors)
					.Include(p => p.ProductPrice)
					.ToListAsync();
				return product;
			}
			catch
			{
				return null;
			}
		}
		public async Task<IEnumerable<ProductTitleDto>> GetProdctuSliders()
		{
			var product = await _shopContext.Products
				.Where(p => p.ShowInSlider == true)
				.Include(p => p.ProductPhotos)
				.Include(p => p.ProductPrice)
				.Include(p => p.Admin)
				.Select(p => ProductMapper.TOProductTitleDto(p))
				.ToListAsync();

			return product;
		}
		public async Task<IEnumerable<ProductTitleDto>> GetProductByGroupID(int id)
		{
			return await _shopContext.Products
				.Where(p => p.Groupid == id)
				.Include(p => p.Admin)
				.Include(p => p.ProductPhotos)
				.Include(p => p.ProductPrice)
				.Select(p => ProductMapper.TOProductTitleDto(p)).AsNoTracking().ToListAsync();
		}
		public async Task<EditProductDto> GetProductByIDForEdit(int id)
		{
			try
			{
				var files = new List<IFormFile>();
				var res = await GetProductByID(id);
				foreach (var item in res.ProductPhotos)
				{
					files.Add(_fileManager.GetFormFile(item.PhotoName, Directory.GetCurrentDirectory() + FileRoots.GetStreamRoot + item.PhotoName));
				}
				return new EditProductDto
				{
					ProductPhotoFiles = files,
					Company = res.Company,
					CreationDate = res.CreationDate,
					Description = res.Description,
					Group = res.GroupProduct,
					Id = res.Id,
					MetaDescription = res.MetaDescription,
					MetaTag = res.MetaTag,
					Off = res.Off,
					Price = res.ProductPrice.Price,
					ProductName = res.ProductName,
					ShortDescription = res.ShortDescription,
					ShowInSlider = res.ShowInSlider,
					Slug = res.Slug,
					Visit = res.Visit,
					SliderPicture = _fileManager.GetFormFile(res.SliderPicture, Directory.GetCurrentDirectory() + FileRoots.GetStreamRoot + res.SliderPicture)
				};
			}
			catch
			{
				return null;
			}
		}
		public async Task<Product> GetProductByID(int id)
		{
			try
			{
				return await _shopContext.Products
				.Include(p => p.GroupProduct)
				.Include(p => p.Admin)
				.Include(p => p.ProductPhotos)
				.Include(p => p.ProductColors)
				.Include(p => p.ProductPrice)
				.Include(p => p.Company)
				.FirstOrDefaultAsync(p => p.Id == id);
			}
			catch
			{
				return null;
			}
		}
		public async Task<List<Product>> MostVisits()
		{
			try
			{
				return await _shopContext.Products
				  .OrderByDescending(p => p.Visit).Take(5)
				  .Include(p => p.GroupProduct)
				  .Include(p => p.Admin).ToListAsync(); ;
			}
			catch
			{
				return null;
			}
		}
		public async Task<IEnumerable<ProductDto>> searchProducts(string txt)
		{
			throw new NotImplementedException();
		}
		public async Task<List<ProductTitleDto>> GetProductByFilter(int groupId, string[] brands = null, string searchOrder = "Latest")
		{
			var QueryList = _shopContext.Products
				.Include(p => p.Admin)
				.Include(p => p.ProductPhotos)
				.Include(p => p.ProductPrice)
				//.Select(p => ProductMapper.TOProductTitleDto(p))
				.AsQueryable();
			if (groupId != 0)
			{
				QueryList = QueryList.Where(g => g.Groupid == groupId);
			}
			if (brands.Length > 0)
			{
				foreach (var Brand in brands)
				{
					int x = Convert.ToInt32(Brand);
					QueryList = QueryList.Where(g => g.CompanyId == x);
				}
			}

			List<Product> resault = await QueryList.ToListAsync();
			List<ProductTitleDto> output = new List<ProductTitleDto>();
			foreach (var p in resault)
			{
				output.Add(ProductMapper.TOProductTitleDto(p));
			}
			switch (searchOrder)
			{

				case "Latest":
					return output.OrderByDescending(l => l.id).ToList();

				case "Most":
					return output.OrderByDescending(l => l.id).ToList();

				case "Most_visited":
					return output.OrderByDescending(l => l.id).ToList();

				case "Cheapest":
					return output.OrderByDescending(l => l.id).ToList();

				case "Most_expensive":
					return output.OrderByDescending(l => l.id).ToList();

				default:
					return output.OrderByDescending(l => l.id).ToList();
			}
		}
		public bool ProductExists(int id)
		{
			return _shopContext.Products.Any(e => e.Id == id);
		}
	}
}