using CORETest.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface ICompanyService
	{
		Task<List<Company>> GetCompanyList();
		Task<OperationResault> InsertCompany(string Name);
	}
}
