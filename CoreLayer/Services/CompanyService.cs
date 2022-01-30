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
	public class CompanyService : ICompanyService
	{
		#region Dependency Injection
		private readonly ShopContext _shopContext;
		public CompanyService(ShopContext shopContext)
		{
			_shopContext = shopContext;
		}
		#endregion

		public async Task<List<Company>> GetCompanyList()
		{
			return await _shopContext.Companies.ToListAsync();
		}
		public async Task<OperationResault> InsertCompany(string Name)
		{
			try
			{
				_shopContext.Companies.Add(new Company()
				{
					CompanyName = Name,
				});
				await _shopContext.SaveChangesAsync();
				return OperationResault.Success();
			}
			catch (Exception ex)
			{
				return OperationResault.Error(ex.Message);
			}
		}
	}
}
