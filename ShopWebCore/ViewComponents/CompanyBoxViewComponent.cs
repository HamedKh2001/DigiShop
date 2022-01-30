using CoreLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ShopWebCore.ViewComponents
{
	public class CompanyBoxViewComponent : ViewComponent
	{
		private readonly ICompanyService _companyService;
		public CompanyBoxViewComponent(ICompanyService companyService)
		{
			_companyService = companyService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			//return View ("CategoryBox", await _companyService.GetCompanyList());
			return await Task.FromResult(View("CompanyBox", _companyService.GetCompanyList()));
		}
	}
}
