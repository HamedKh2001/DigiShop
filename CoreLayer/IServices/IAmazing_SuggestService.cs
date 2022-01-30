using CoreLayer.DTOs;
using CoreLayer.Services;
using CORETest.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.IServices
{
	public interface IAmazing_SuggestService
	{
		Task<OperationResault> AddSuggestasync(Amazing_Suggest suggest);
		OperationResault EditSuggest(Amazing_Suggest suggest);
		Task<OperationResault> DeleteSuggestasync(int Id);
		Task<List<ProductTitleDto>> GetActiveSuggestsasync();
		Task<List<Amazing_Suggest>> GetAllSuggestsasync();
		Task<Amazing_Suggest> GetSuggestByIDasync(int Id);
	}
}
