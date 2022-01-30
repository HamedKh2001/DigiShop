using CoreLayer.DTOs;
using CORETest.Utilities;
using DataLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IProductService
	{
		Task<OperationResault> DeleteProductColor(ProductColor color);
		Task<OperationResault> DeleteProductPhoto(string photo);
		Task<OperationResault> InsertProduct(ProductDto productDto);
		Task<Product> GetProductByID(int id);
		Task<OperationResault> DeleteProductByID(int id);
		Task<OperationResault> EditProduct(EditProductDto product);
		Task<IEnumerable<Product>> GetAllProducts();
		Task<IEnumerable<ProductTitleDto>> GetProdctuSliders();
		Task<IEnumerable<ProductTitleDto>> GetProductByGroupID(int id);
		Task<EditProductDto> GetProductByIDForEdit(int id);
		Task<List<Product>> MostVisits();
		Task<IEnumerable<ProductDto>> searchProducts(string txt);
		Task<List<ProductTitleDto>> GetProductByFilter(int groupId, string[] brands = null, string searchOrder = "Latest");
		bool ProductExists(int id);
	}
}
